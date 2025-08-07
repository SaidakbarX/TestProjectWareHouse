import React, { useState } from 'react';
import { Plus, Edit, Archive, RotateCcw, Trash2,MapPin } from 'lucide-react';
import { useActiveClients, useArchivedClients, useClientMutations } from '../hooks/useApi';
import { Client, ClientFormData } from '../types';
import { Modal } from '../components/Modal';
import { Button } from '../components/Button';
import { LoadingSpinner } from '../components/LoadingSpinner';
import { ErrorMessage } from '../components/ErrorMessage';
import { TabBar } from '../components/TabBar';

const ClientForm: React.FC<{
  client?: Client;
  onSubmit: (data: ClientFormData) => void;
  onCancel: () => void;
  loading?: boolean;
}> = ({ client, onSubmit, onCancel, loading }) => {
  const [formData, setFormData] = useState<ClientFormData>({
    name: client?.name || '',
    address: client?.address || '',
    isArchived:client?.isArchived || false,
  });

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSubmit(formData);
  };

  return (
    <form onSubmit={handleSubmit} className="space-y-4">
      <div>
        <label className="block text-sm font-medium text-gray-700 mb-1">Name</label>
        <input
          type="text"
          required
          value={formData.name}
          onChange={(e) => setFormData({ ...formData, name: e.target.value })}
          className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
        />
      </div>
      


      <div>
        <label className="block text-sm font-medium text-gray-700 mb-1">Address</label>
        <textarea
          value={formData.address}
          onChange={(e) => setFormData({ ...formData, address: e.target.value })}
          rows={3}
          className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
        />
      </div>

      <div className="flex justify-end space-x-3 pt-4">
        <Button variant="secondary" onClick={onCancel} disabled={loading}>
          Cancel
        </Button>
        <Button type="submit" loading={loading}>
          {client ? 'Update' : 'Create'}
        </Button>
      </div>
    </form>
  );
};

export const ClientsPage: React.FC = () => {
  const [activeTab, setActiveTab] = useState('active');
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [editingClient, setEditingClient] = useState<Client | null>(null);

  const activeQuery = useActiveClients();
  const archivedQuery = useArchivedClients();
  const mutations = useClientMutations();

  const currentQuery = activeTab === 'active' ? activeQuery : archivedQuery;
  const clients = currentQuery.data || [];

  const tabs = [
    { id: 'active', label: 'Active', count: activeQuery.data?.length || 0 },
    { id: 'archived', label: 'Archived', count: archivedQuery.data?.length || 0 },
  ];

  const handleCreate = () => {
    setEditingClient(null);
    setIsModalOpen(true);
  };

  const handleEdit = (client: Client) => {
    setEditingClient(client);
    setIsModalOpen(true);
  };

  const handleSubmit = async (data: ClientFormData) => {
    try {
      if (editingClient) {
        await mutations.update.mutateAsync({ id: editingClient.id, data });
      } else {
        await mutations.create.mutateAsync(data);
      }
      setIsModalOpen(false);
      setEditingClient(null);
    } catch (error) {
      console.error('Error saving client:', error);
    }
  };

  const handleArchive = async (id: number) => {
    if (window.confirm('Are you sure you want to archive this client?')) {
      await mutations.archive.mutateAsync(id);
    }
  };

  const handleRestore = async (id: number) => {
    if (window.confirm('Are you sure you want to restore this client?')) {
      await mutations.restore.mutateAsync(id);
    }
  };

  const handleDelete = async (id: number) => {
    if (window.confirm('Are you sure you want to permanently delete this client?')) {
      await mutations.delete.mutateAsync(id);
    }
  };

  return (
    <div>
      <div className="flex justify-between items-center mb-6">
        <h1 className="text-2xl font-bold text-gray-900">Clients</h1>
        <Button icon={Plus} onClick={handleCreate}>
          Add Client
        </Button>
      </div>

      <TabBar tabs={tabs} activeTab={activeTab} onTabChange={setActiveTab} />

      {currentQuery.isLoading ? (
        <LoadingSpinner />
      ) : currentQuery.error ? (
        <ErrorMessage message="Failed to load clients" />
      ) : (
        <div className="bg-white rounded-lg shadow overflow-hidden">
          <table className="min-w-full divide-y divide-gray-200">
            <thead className="bg-gray-50">
              <tr>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Client
                </th>
              </tr>
            </thead>
            <tbody className="bg-white divide-y divide-gray-200">
              {clients.map((client) => (
                <tr key={client.id} className="hover:bg-gray-50">
                  <td className="px-6 py-4">
                    <div>
                      <div className="text-sm font-medium text-gray-900">{client.name}</div>
                      {client.address && (
                        <div className="flex items-center text-sm text-gray-500 mt-1">
                          <MapPin className="h-4 w-4 mr-1" />
                          {client.address}
                        </div>
                      )}
                    </div>
                  </td>
                  <td className="px-6 py-4 whitespace-nowrap text-right text-sm font-medium">
                    <div className="flex justify-end space-x-2">
                      <Button size="sm" variant="secondary" icon={Edit} onClick={() => handleEdit(client)}>
                        Edit
                      </Button>
                      {activeTab === 'active' ? (
                        <Button size="sm" variant="warning" icon={Archive} onClick={() => handleArchive(client.id)}>
                          Archive
                        </Button>
                      ) : (
                        <>
                          <Button size="sm" variant="success" icon={RotateCcw} onClick={() => handleRestore(client.id)}>
                            Restore
                          </Button>
                          <Button size="sm" variant="danger" icon={Trash2} onClick={() => handleDelete(client.id)}>
                            Delete
                          </Button>
                        </>
                      )}
                    </div>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
          
          {clients.length === 0 && (
            <div className="text-center py-12">
              <p className="text-gray-500">No {activeTab} clients found.</p>
            </div>
          )}
        </div>
      )}

      <Modal
        isOpen={isModalOpen}
        onClose={() => setIsModalOpen(false)}
        title={editingClient ? 'Edit Client' : 'Add Client'}
      >
        <ClientForm
          client={editingClient?? undefined}
          onSubmit={handleSubmit}
          onCancel={() => setIsModalOpen(false)}
          loading={mutations.create.isPending || mutations.update.isPending}
        />
      </Modal>
    </div>
  );
};