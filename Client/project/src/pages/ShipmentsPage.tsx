import React, { useState } from 'react';
import { Plus, Edit, Trash2, Eye, FileSignature, RotateCcw } from 'lucide-react';
import { 
  useShipmentDocuments, 
  useShipmentDocumentMutations, 
  useActiveResources, 
  useActiveMeasurements, 
  useActiveClients 
} from '../hooks/useApi';
import { ShipmentDocument, ShipmentDocumentFormData, ShipmentItem } from '../types';
import { Modal } from '../components/Modal';
import { Button } from '../components/Button';
import { LoadingSpinner } from '../components/LoadingSpinner';
import { ErrorMessage } from '../components/ErrorMessage';

const ShipmentDocumentForm: React.FC<{
  document?: ShipmentDocument;
  onSubmit: (data: ShipmentDocumentFormData) => void;
  onCancel: () => void;
  loading?: boolean;
}> = ({ document, onSubmit, onCancel, loading }) => {
  const { data: clients = [] } = useActiveClients();
  const { data: resources = [] } = useActiveResources();
  const { data: measurements = [] } = useActiveMeasurements();

  const [formData, setFormData] = useState<ShipmentDocumentFormData>({
  clientId: document?.clientId || 0,
  number: document?.number || '',
  date: document?.date.split('T')[0] || new Date().toISOString().split('T')[0],
  status: document?.status || 0,
  items: document?.items.map(item => ({
    resourceId: item.resourceId,
    measurementId: item.measurementId,
    quantity: item.quantity
  })) || [],
});



  const addItem = () => {
    setFormData({
      ...formData,
      items: [...formData.items, {
        resourceId: 0,
        measurementId: 0,
        quantity: 0,
      }],
    });
  };





  const removeItem = (index: number) => {
    setFormData({
      ...formData,
      items: formData.items.filter((_, i) => i !== index),
    });
  };

  const updateItem = (index: number, field: keyof ShipmentItem, value: number) => {
    const newItems = [...formData.items];
    newItems[index] = { ...newItems[index], [field]: value };
    setFormData({ ...formData, items: newItems });
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSubmit(formData);
  };

  return (
    <form onSubmit={handleSubmit} className="space-y-6 max-h-96 overflow-y-auto">
      <div className="grid grid-cols-2 gap-4">
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">Client</label>
          <select
            required
            value={formData.clientId}
            onChange={(e) => setFormData({ ...formData, clientId: parseInt(e.target.value) })}
            className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
          >
            <option value={0}>Select Client</option>
            {clients.map(client => (
              <option key={client.id} value={client.id}>{client.name}</option>
            ))}
          </select>
        </div>

        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">Document Number</label>
          <input
            type="text"
            required
            value={formData.number}
            onChange={(e) => setFormData({ ...formData, number: e.target.value })}
            className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
        </div>
      </div>

      <div>
        <label className="block text-sm font-medium text-gray-700 mb-1">Document Date</label>
        <input
          type="date"
          required
          value={formData.date}
          onChange={(e) => setFormData({ ...formData, date: e.target.value })}
          className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
        />
      </div>

      <div>
        <div className="flex items-center justify-between mb-2">
          <label className="block text-sm font-medium text-gray-700">Items</label>
          <Button type="button" size="sm" onClick={addItem}>Add Item</Button>
        </div>
        
        <div className="space-y-2">
          {formData.items.map((item, index) => (
            <div key={index} className="grid grid-cols-6 gap-2 items-end">
              <div>
                <select
                  required
                  value={item.resourceId}
                  onChange={(e) => updateItem(index, 'resourceId', parseInt(e.target.value))}
                  className="w-full px-2 py-1 text-sm border border-gray-300 rounded focus:outline-none focus:ring-1 focus:ring-blue-500"
                >
                  <option value={0}>Resource</option>
                  {resources.map(resource => (
                    <option key={resource.id} value={resource.id}>{resource.name}</option>
                  ))}
                </select>
              </div>
              
              <div>
                <select
                  required
                  value={item.measurementId}
                  onChange={(e) => updateItem(index, 'measurementId', parseInt(e.target.value))}
                  className="w-full px-2 py-1 text-sm border border-gray-300 rounded focus:outline-none focus:ring-1 focus:ring-blue-500"
                >
                  <option value={0}>Unit</option>
                  {measurements.map(measurement => (
                    <option key={measurement.id} value={measurement.id}>{measurement.name}</option>
                  ))}
                </select>
              </div>
              
              <div>
                <input
                  type="number"
                  step="0"
                  placeholder="Qty"
                  value={item.quantity}
                  onChange={(e) => updateItem(index, 'quantity', parseFloat(e.target.value) || 0)}
                  className="w-full px-2 py-1 text-sm border border-gray-300 rounded focus:outline-none focus:ring-1 focus:ring-blue-500"
                />
              </div>
              
              <div>
                <Button type="button" size="sm" variant="danger" onClick={() => removeItem(index)}>
                  ×
                </Button>
              </div>
            </div>
          ))}
        </div>
      </div>

      <div className="flex justify-end space-x-3 pt-4 border-t">
        <Button variant="secondary" onClick={onCancel} disabled={loading}>
          Cancel
        </Button>
        <Button type="submit" loading={loading}>
          {document ? 'Update' : 'Create'}
        </Button>
      </div>
    </form>
  );
};

export const ShipmentsPage: React.FC = () => {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [editingDocument, setEditingDocument] = useState<ShipmentDocument | null>(null);
  const [viewingDocument, setViewingDocument] = useState<ShipmentDocument | null>(null);

  const documentsQuery = useShipmentDocuments();
  const mutations = useShipmentDocumentMutations();

  const documents = documentsQuery.data || [];

  const handleCreate = () => {
    setEditingDocument(null);
    setIsModalOpen(true);
  };

  const handleEdit = (document: ShipmentDocument) => {
    setEditingDocument(document);
    setIsModalOpen(true);
  };

  const handleView = (document: ShipmentDocument) => {
    setViewingDocument(document);
  };

  const handleSubmit = async (data: ShipmentDocumentFormData) => {
    try {
      if (editingDocument) {
        await mutations.update.mutateAsync({ id: editingDocument.id, data });
      } else {
        await mutations.create.mutateAsync(data);
      }
      setIsModalOpen(false);
      setEditingDocument(null);
    } catch (error) {
      console.error('Error saving shipment document:', error);
    }
  };

  const handleSign = async (id: number) => {
    if (window.confirm('Are you sure you want to sign this shipment document?')) {
      await mutations.sign.mutateAsync(id);
    }
  };

  const handleRevoke = async (id: number) => {
    if (window.confirm('Are you sure you want to revoke the signature from this shipment document?')) {
      await mutations.revoke.mutateAsync(id);
    }
  };

  const handleDelete = async (id: number) => {
    if (window.confirm('Are you sure you want to delete this shipment document?')) {
      await mutations.delete.mutateAsync(id);
    }
  };

  function renderStatusBadge(status: number): React.ReactNode {
    const statusMap: Record<number, { text: string; className: string }> = {
      0: { text: 'Pending Signature', className: 'bg-yellow-100 text-yellow-800' },
      1: { text: 'Signed', className: 'bg-green-100 text-green-800' },
      2: { text: 'Revoked', className: 'bg-red-100 text-red-800' },
    };
    const s = statusMap[status] || statusMap[0];
    return (
      <span className={`inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium ${s.className}`}>
        {s.text}
      </span>
    );
  }
  return (
    <div>
      <div className="flex justify-between items-center mb-6">
        <h1 className="text-2xl font-bold text-gray-900">Shipment Documents</h1>
        <Button icon={Plus} onClick={handleCreate}>
          New Shipment
        </Button>
      </div>

      {documentsQuery.isLoading ? (
        <LoadingSpinner />
      ) : documentsQuery.error ? (
        <ErrorMessage message="Failed to load shipment documents" />
      ) : (
        <div className="bg-white rounded-lg shadow overflow-hidden">
          <table className="min-w-full divide-y divide-gray-200">
            <thead className="bg-gray-50">
              <tr>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Document
                </th>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Client
                </th>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Status
                </th>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Items
                </th>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Date
                </th>
                <th className="px-6 py-3 text-right text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Actions
                </th>
              </tr>
            </thead>
            <tbody className="bg-white divide-y divide-gray-200">
  {documents.map((document) => (
    <tr key={document.id} className="hover:bg-gray-50">
      <td className="px-6 py-4 whitespace-nowrap">
        <div className="text-sm font-medium text-gray-900">{document.number}</div>
      </td>
      <td className="px-6 py-4 whitespace-nowrap">
        <div className="text-sm text-gray-900">{document.clientName}</div>
      </td>
      <td className="px-6 py-4 whitespace-nowrap">
        {renderStatusBadge(document.status)}
      </td>

      <td className="px-6 py-4 whitespace-nowrap">
        <div className="text-sm text-gray-900">{document.items.length} items</div>
      </td>
      <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
        {new Date(document.date).toLocaleDateString()}
      </td>
      <td className="px-6 py-4 whitespace-nowrap text-right text-sm font-medium">
        <div className="flex justify-end space-x-2">
          <Button size="sm" variant="secondary" icon={Eye} onClick={() => handleView(document)}>
            View
          </Button>

          {!document.isSigned && (
            <Button size="sm" variant="secondary" icon={Edit} onClick={() => handleEdit(document)}>
              Edit
            </Button>
          )}

          {document.status === 1 ? (
  <Button
    size="sm"
    variant="warning"
    icon={RotateCcw}
    onClick={() => handleRevoke(document.id)}
  >
    Revoke
  </Button>
) : (
  <Button
    size="sm"
    variant="success"
    icon={FileSignature}
    onClick={() => handleSign(document.id)}
  >
    Sign
  </Button>
)}


          <Button
            size="sm"
            variant="danger"
            icon={Trash2}
            onClick={() => handleDelete(document.id)}
          >
            Delete
          </Button>
        </div>
      </td>
    </tr>
  ))}
</tbody>

          </table>
          
          {documents.length === 0 && (
            <div className="text-center py-12">
              <p className="text-gray-500">No shipment documents found.</p>
            </div>
          )}
        </div>
      )}

      <Modal
        isOpen={isModalOpen}
        onClose={() => setIsModalOpen(false)}
        title={editingDocument ? 'Edit Shipment Document' : 'New Shipment Document'}
      >
        <ShipmentDocumentForm
          document={editingDocument??undefined}
          onSubmit={handleSubmit}
          onCancel={() => setIsModalOpen(false)}
          loading={mutations.create.isPending || mutations.update.isPending}
        />
      </Modal>

      <Modal
        isOpen={!!viewingDocument}
        onClose={() => setViewingDocument(null)}
        title="Shipment Document Details"
      >
        {viewingDocument && (
          <div className="space-y-4">
            <div className="grid grid-cols-2 gap-4">
              <div>
                <label className="block text-sm font-medium text-gray-700">Document Number</label>
                <p className="text-sm text-gray-900">{viewingDocument.number}</p>
              </div>
              <div>
                <label className="block text-sm font-medium text-gray-700">Client</label>
                <p className="text-sm text-gray-900">{viewingDocument.clientName}</p>
              </div>
              <div>
                <label className="block text-sm font-medium text-gray-700">Date</label>
                <p className="text-sm text-gray-900">{new Date(viewingDocument.date).toLocaleDateString()}</p>
              </div>
              <div>
                <label className="block text-sm font-medium text-gray-700">Status</label>
                <div>
{viewingDocument.status === 1 ? (
  <span className="inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium bg-green-100 text-green-800">
    Signed
  </span>
) : viewingDocument.status === 2 ? (
  <span className="inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium bg-red-100 text-red-800">
    Revoked
  </span>
) : (
  <span className="inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium bg-yellow-100 text-yellow-800">
    Pending Signature
  </span>
)}

                </div>
              </div>
            </div>
            
            
            <div>
              <label className="block text-sm font-medium text-gray-700 mb-2">Items</label>
              <div className="overflow-x-auto">
                <table className="min-w-full divide-y divide-gray-200">
                  <thead className="bg-gray-50">
                    <tr>
                      <th className="px-3 py-2 text-left text-xs font-medium text-gray-500 uppercase">Resource</th>
                      <th className="px-3 py-2 text-left text-xs font-medium text-gray-500 uppercase">Unit</th>
                      <th className="px-3 py-2 text-right text-xs font-medium text-gray-500 uppercase">Qty</th>
                    </tr>
                  </thead>
                  <tbody className="bg-white divide-y divide-gray-200">
                    {viewingDocument.items.map((item, index) => (
                      <tr key={index}>
                        <td className="px-3 py-2 text-sm text-gray-900">{item.resourceName}</td>
                        <td className="px-3 py-2 text-sm text-gray-900">{item.measurementName}</td>
                        <td className="px-3 py-2 text-sm text-gray-900 text-right">{item.quantity}</td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            </div>
          </div>
        )}
      </Modal>
    </div>
  );
};