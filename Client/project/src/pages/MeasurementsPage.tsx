import React, { useState } from 'react';
import { Plus, Edit, Archive, RotateCcw, Trash2 } from 'lucide-react';
import { useActiveMeasurements, useArchivedMeasurements, useMeasurementMutations } from '../hooks/useApi';
import { Measurement, MeasurementFormData } from '../types';
import { Modal } from '../components/Modal';
import { Button } from '../components/Button';
import { LoadingSpinner } from '../components/LoadingSpinner';
import { ErrorMessage } from '../components/ErrorMessage';
import { TabBar } from '../components/TabBar';

const MeasurementForm: React.FC<{
  measurement?: Measurement;
  onSubmit: (data: MeasurementFormData) => void;
  onCancel: () => void;
  loading?: boolean;
}> = ({ measurement, onSubmit, onCancel, loading }) => {
  const [formData, setFormData] = useState<MeasurementFormData>({
    name: measurement?.name || '',
    isArchived: measurement?.isArchived || false,
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
          {measurement ? 'Update' : 'Create'}
        </Button>
      </div>
    </form>
  );
};

export const MeasurementsPage: React.FC = () => {
  const [activeTab, setActiveTab] = useState('active');
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [editingMeasurement, setEditingMeasurement] = useState<Measurement | null>(null);

  const activeQuery = useActiveMeasurements();
  const archivedQuery = useArchivedMeasurements();
  const mutations = useMeasurementMutations();

  const currentQuery = activeTab === 'active' ? activeQuery : archivedQuery;
  const measurements = currentQuery.data || [];

  const tabs = [
    { id: 'active', label: 'Active', count: activeQuery.data?.length || 0 },
    { id: 'archived', label: 'Archived', count: archivedQuery.data?.length || 0 },
  ];

  const handleCreate = () => {
    setEditingMeasurement(null);
    setIsModalOpen(true);
  };

  const handleEdit = (measurement: Measurement) => {
    setEditingMeasurement(measurement);
    setIsModalOpen(true);
  };

  const handleSubmit = async (data: MeasurementFormData) => {
    try {
      if (editingMeasurement) {
        await mutations.update.mutateAsync({ id: editingMeasurement.id, data });
      } else {
        await mutations.create.mutateAsync(data);
      }
      setIsModalOpen(false);
      setEditingMeasurement(null);
    } catch (error) {
      console.error('Error saving measurement:', error);
    }
  };

  const handleArchive = async (id: number) => {
    if (window.confirm('Are you sure you want to archive this measurement?')) {
      await mutations.archive.mutateAsync(id);
    }
  };

  const handleRestore = async (id: number) => {
    if (window.confirm('Are you sure you want to restore this measurement?')) {
      await mutations.restore.mutateAsync(id);
    }
  };

  const handleDelete = async (id: number) => {
    if (window.confirm('Are you sure you want to permanently delete this measurement?')) {
      await mutations.delete.mutateAsync(id);
    }
  };

  return (
    <div>
      <div className="flex justify-between items-center mb-6">
        <h1 className="text-2xl font-bold text-gray-900">Measurements</h1>
        <Button icon={Plus} onClick={handleCreate}>
          Add Measurement
        </Button>
      </div>

      <TabBar tabs={tabs} activeTab={activeTab} onTabChange={setActiveTab} />

      {currentQuery.isLoading ? (
        <LoadingSpinner />
      ) : currentQuery.error ? (
        <ErrorMessage message="Failed to load measurements" />
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
              {measurements.map((measurement) => (
                <tr key={measurement.id} className="hover:bg-gray-50">
                  <td className="px-6 py-4 whitespace-nowrap">
                    <div className="text-sm font-medium text-gray-900">{measurement.name}</div>
                  </td>
                  <td className="px-6 py-4 whitespace-nowrap text-right text-sm font-medium">
                    <div className="flex justify-end space-x-2">
                      <Button size="sm" variant="secondary" icon={Edit} onClick={() => handleEdit(measurement)}>
                        Edit
                      </Button>
                      {activeTab === 'active' ? (
                        <Button size="sm" variant="warning" icon={Archive} onClick={() => handleArchive(measurement.id)}>
                          Archive
                        </Button>
                      ) : (
                        <>
                          <Button size="sm" variant="success" icon={RotateCcw} onClick={() => handleRestore(measurement.id)}>
                            Restore
                          </Button>
                          <Button size="sm" variant="danger" icon={Trash2} onClick={() => handleDelete(measurement.id)}>
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
          
          {measurements.length === 0 && (
            <div className="text-center py-12">
              <p className="text-gray-500">No {activeTab} measurements found.</p>
            </div>
          )}
        </div>
      )}

      <Modal
        isOpen={isModalOpen}
        onClose={() => setIsModalOpen(false)}
        title={editingMeasurement ? 'Edit Measurement' : 'Add Measurement'}
      >
        <MeasurementForm
          measurement={editingMeasurement??undefined}
          onSubmit={handleSubmit}
          onCancel={() => setIsModalOpen(false)}
          loading={mutations.create.isPending || mutations.update.isPending}
        />
      </Modal>
    </div>
  );
};