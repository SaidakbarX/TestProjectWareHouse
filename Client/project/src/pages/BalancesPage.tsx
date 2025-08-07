import React from 'react';
import { BarChart3, Package } from 'lucide-react';
import { useBalances } from '../hooks/useApi';
import { LoadingSpinner } from '../components/LoadingSpinner';
import { ErrorMessage } from '../components/ErrorMessage';

export const BalancesPage: React.FC = () => {
  const balancesQuery = useBalances();
  const balances = balancesQuery.data || [];

  const totalItems = balances.reduce((sum, balance) => sum + (balance.quantity ?? 0), 0);

  return (
    <div>
      <div className="flex items-center justify-between mb-6">
        <h1 className="text-2xl font-bold text-gray-900">Inventory Balances</h1>
        <div className="flex items-center space-x-2 text-sm text-gray-500">
          <BarChart3 className="h-4 w-4" />
          <span>Read-only view</span>
        </div>
      </div>

      {/* Summary Cards */}
      <div className="grid grid-cols-1 md:grid-cols-3 gap-6 mb-6">
        <div className="bg-white rounded-lg shadow p-6">
          <div className="flex items-center">
            <div className="p-2 bg-blue-100 rounded-md">
              <Package className="h-6 w-6 text-blue-600" />
            </div>
            <div className="ml-4">
              <p className="text-sm font-medium text-gray-500">Total Resources</p>
              <p className="text-2xl font-bold text-gray-900">{balances.length}</p>
            </div>
          </div>
        </div>

        <div className="bg-white rounded-lg shadow p-6">
          <div className="flex items-center">
            <div className="p-2 bg-green-100 rounded-md">
              <BarChart3 className="h-6 w-6 text-green-600" />
            </div>
            <div className="ml-4">
              <p className="text-sm font-medium text-gray-500">Total Quantity</p>
              <p className="text-2xl font-bold text-gray-900">{totalItems.toFixed(2)}</p>
            </div>
          </div>
        </div>

        <div className="bg-white rounded-lg shadow p-6">
          <div className="flex items-center">
            <div className="p-2 bg-amber-100 rounded-md">
            </div>
          </div>
        </div>
      </div>

      {balancesQuery.isLoading ? (
        <LoadingSpinner />
      ) : balancesQuery.error ? (
        <ErrorMessage message="Failed to load balances" />
      ) : (
        <div className="bg-white rounded-lg shadow overflow-hidden">
          <div className="px-6 py-4 border-b border-gray-200">
            <h2 className="text-lg font-medium text-gray-900">Resource Balances</h2>
          </div>

          <table className="min-w-full divide-y divide-gray-200">
            <thead className="bg-gray-50">
              <tr>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Resource
                </th>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Unit
                </th>
                <th className="px-6 py-3 text-right text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Quantity
                </th>
              </tr>
            </thead>
            <tbody className="bg-white divide-y divide-gray-200">
              {balances.map((balance, index) => (
                <tr key={index} className="hover:bg-gray-50">
                  <td className="px-6 py-4 whitespace-nowrap">
                    <div className="text-sm font-medium text-gray-900">{balance.resourceName}</div>
                  </td>
                  <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-900">
                    {balance.measurementName}
                  </td>
                  <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-900 text-right">
                    {(balance.quantity ?? 0).toFixed(2)}
                  </td>
                </tr>
              ))}
            </tbody>
          </table>

          {balances.length === 0 && (
            <div className="text-center py-12">
              <Package className="mx-auto h-12 w-12 text-gray-400" />
              <h3 className="mt-2 text-sm font-medium text-gray-900">No inventory balances</h3>
              <p className="mt-1 text-sm text-gray-500">
                Start by creating reception documents to populate your inventory.
              </p>
            </div>
          )}
        </div>
      )}
    </div>
  );
};
