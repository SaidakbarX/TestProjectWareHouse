import React, { useState } from 'react';
import { Plus, Edit, Archive, RotateCcw, Trash2 } from 'lucide-react';
import { useActiveResources, useArchivedResources, useResourceMutations } from '../hooks/useApi';
import { Resource, ResourceFormData } from '../types';
import { Modal } from '../components/Modal';
import { Button } from '../components/Button';
import { LoadingSpinner } from '../components/LoadingSpinner';
import { ErrorMessage } from '../components/ErrorMessage';
import { TabBar } from '../components/TabBar';

const ResourceForm: React.FC<{
  resource?: Resource;
  onSubmit: (data: ResourceFormData) => void;
  onCancel: () => void;
  loading?: boolean;
}> = ({ resource, onSubmit, onCancel, loading }) => {
  const [formData, setFormData] = useState<ResourceFormData>({
    name: resource?.name || '',
    isArchived: resource?.isArchived || false,
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
      

      <div className="flex justify-end space-x-3 pt-4">
        <Button variant="secondary" onClick={onCancel} disabled={loading}>
          Cancel
        </Button>
        <Button type="submit" loading={loading}>
          {resource ? 'Update' : 'Create'}
        </Button>
      </div>
    </form>
  );
};

export const ResourcesPage: React.FC = () => {
  const [activeTab, setActiveTab] = useState('active');
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [editingResource, setEditingResource] = useState<Resource | null>(null);

  const activeQuery = useActiveResources();
  const archivedQuery = useArchivedResources();
  const mutations = useResourceMutations();

  const currentQuery = activeTab === 'active' ? activeQuery : archivedQuery;
  const resources = currentQuery.data || [];

  const tabs = [
    { id: 'active', label: 'Active', count: activeQuery.data?.length || 0 },
    { id: 'archived', label: 'Archived', count: archivedQuery.data?.length || 0 },
  ];

  const handleCreate = () => {
    setEditingResource(null);
    setIsModalOpen(true);
  };

  const handleEdit = (resource: Resource) => {
    setEditingResource(resource);
    setIsModalOpen(true);
  };

  const handleSubmit = async (data: ResourceFormData) => {
    try {
      if (editingResource) {
        await mutations.update.mutateAsync({ id: editingResource.id, data });
      } else {
        await mutations.create.mutateAsync(data);
      }
      setIsModalOpen(false);
      setEditingResource(null);
    } catch (error) {
      console.error('Error saving resource:', error);
    }
  };

  const handleArchive = async (id: number) => {
    if (window.confirm('Are you sure you want to archive this resource?')) {
      await mutations.archive.mutateAsync(id);
    }
  };

  const handleRestore = async (id: number) => {
    if (window.confirm('Are you sure you want to restore this resource?')) {
      await mutations.restore.mutateAsync(id);
    }
  };

  const handleDelete = async (id: number) => {
    if (window.confirm('Are you sure you want to permanently delete this resource?')) {
      await mutations.delete.mutateAsync(id);
    }
  };

  return (
    <div>
      <div className="flex justify-between items-center mb-6">
        <h1 className="text-2xl font-bold text-gray-900">Resources</h1>
        <Button icon={Plus} onClick={handleCreate}>
          Add Resource
        </Button>
      </div>

      <TabBar tabs={tabs} activeTab={activeTab} onTabChange={setActiveTab} />

      {currentQuery.isLoading ? (
        <LoadingSpinner />
      ) : currentQuery.error ? (
        <ErrorMessage message="Failed to load resources" />
      ) : (
        <div className="bg-white rounded-lg shadow overflow-hidden">
          <table className="min-w-full divide-y divide-gray-200">
            <thead className="bg-gray-50">
              <tr>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Name
                </th>
              </tr>
            </thead>
            <tbody className="bg-white divide-y divide-gray-200">
              {resources.map((resource) => (
                <tr key={resource.id} className="hover:bg-gray-50">
                  <td className="px-6 py-4 whitespace-nowrap">
                    <div className="text-sm font-medium text-gray-900">{resource.name}</div>
                  </td>
                  <td className="px-6 py-4">
                    <div className="text-sm text-gray-500">{resource.description}</div>
                  </td>
                  <td className="px-6 py-4 whitespace-nowrap text-right text-sm font-medium">
                    <div className="flex justify-end space-x-2">
                      <Button size="sm" variant="secondary" icon={Edit} onClick={() => handleEdit(resource)}>
                        Edit
                      </Button>
                      {activeTab === 'active' ? (
                        <Button size="sm" variant="warning" icon={Archive} onClick={() => handleArchive(resource.id)}>
                          Archive
                        </Button>
                      ) : (
                        <>
                          <Button size="sm" variant="success" icon={RotateCcw} onClick={() => handleRestore(resource.id)}>
                            Restore
                          </Button>
                          <Button size="sm" variant="danger" icon={Trash2} onClick={() => handleDelete(resource.id)}>
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
          
          {resources.length === 0 && (
            <div className="text-center py-12">
              <p className="text-gray-500">No {activeTab} resources found.</p>
            </div>
          )}
        </div>
      )}

      <Modal
        isOpen={isModalOpen}
        onClose={() => setIsModalOpen(false)}
        title={editingResource ? 'Edit Resource' : 'Add Resource'}
      >
        <ResourceForm
          resource={editingResource ?? undefined}
          onSubmit={handleSubmit}
          onCancel={() => setIsModalOpen(false)}
          loading={mutations.create.isPending || mutations.update.isPending}
        />
      </Modal>
    </div>
  );
};