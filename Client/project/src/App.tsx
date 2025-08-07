import React from 'react';
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { Layout } from './components/Layout';
import { ResourcesPage } from './pages/ResourcesPage';
import { MeasurementsPage } from './pages/MeasurementsPage';
import { ClientsPage } from './pages/ClientsPage';
import { ReceptionsPage } from './pages/ReceptionsPage';
import { ShipmentsPage } from './pages/ShipmentsPage';
import { BalancesPage } from './pages/BalancesPage';

const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      retry: 1,
      staleTime: 1000 * 60 * 5, // 5 minutes
    },
  },
});

function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <BrowserRouter>
        <Layout>
          <Routes>
            <Route path="/" element={<Navigate to="/resources" replace />} />
            <Route path="/resources" element={<ResourcesPage />} />
            <Route path="/measurements" element={<MeasurementsPage />} />
            <Route path="/clients" element={<ClientsPage />} />
            <Route path="/receptions" element={<ReceptionsPage />} />
            <Route path="/shipments" element={<ShipmentsPage />} />
            <Route path="/balances" element={<BalancesPage />} />
          </Routes>
        </Layout>
      </BrowserRouter>
    </QueryClientProvider>
  );
}

export default App;