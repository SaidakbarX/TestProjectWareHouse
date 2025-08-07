import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { 
  resourcesApi, 
  measurementsApi, 
  clientsApi, 
  receptionDocumentsApi, 
  shipmentDocumentsApi, 
  balancesApi 
} from '../services/api';

// Resources hooks
export const useActiveResources = () => {
  return useQuery({
    queryKey: ['resources', 'active'],
    queryFn: () => resourcesApi.getActive().then(res => res.data),
  });
};



export const useArchivedResources = () => {
  return useQuery({
    queryKey: ['resources', 'archived'],
    queryFn: () => resourcesApi.getArchived().then(res => res.data),
  });
};

export const useResourceMutations = () => {
  const queryClient = useQueryClient();
  
  const invalidateResources = () => {
    queryClient.invalidateQueries({ queryKey: ['resources'] });
  };

  return {
    create: useMutation({
      mutationFn: resourcesApi.create,
      onSuccess: invalidateResources,
    }),
    update: useMutation({
      mutationFn: ({ id, data }: { id: number; data: any }) => resourcesApi.update(id, data),
      onSuccess: invalidateResources,
    }),
    archive: useMutation({
      mutationFn: resourcesApi.archive,
      onSuccess: invalidateResources,
    }),
    restore: useMutation({
      mutationFn: resourcesApi.restore,
      onSuccess: invalidateResources,
    }),
    delete: useMutation({
      mutationFn: resourcesApi.delete,
      onSuccess: invalidateResources,
    }),
  };
};



// Measurements hooks
export const useActiveMeasurements = () => {
  return useQuery({
    queryKey: ['measurements', 'active'],
    queryFn: () => measurementsApi.getActive().then(res => res.data),
  });
};

export const useArchivedMeasurements = () => {
  return useQuery({
    queryKey: ['measurements', 'archived'],
    queryFn: () => measurementsApi.getArchived().then(res => res.data),
  });
};

export const useMeasurementMutations = () => {
  const queryClient = useQueryClient();
  
  const invalidateMeasurements = () => {
    queryClient.invalidateQueries({ queryKey: ['measurements'] });
  };

  return {
    create: useMutation({
      mutationFn: measurementsApi.create,
      onSuccess: invalidateMeasurements,
    }),
    update: useMutation({
      mutationFn: ({ id, data }: { id: number; data: any }) => measurementsApi.update(id, data),
      onSuccess: invalidateMeasurements,
    }),
    archive: useMutation({
      mutationFn: measurementsApi.archive,
      onSuccess: invalidateMeasurements,
    }),
    restore: useMutation({
      mutationFn: measurementsApi.restore,
      onSuccess: invalidateMeasurements,
    }),
    delete: useMutation({
      mutationFn: measurementsApi.delete,
      onSuccess: invalidateMeasurements,
    }),
  };
};

// Clients hooks
export const useActiveClients = () => {
  return useQuery({
    queryKey: ['clients', 'active'],
    queryFn: () => clientsApi.getActive().then(res => res.data),
  });
};

export const useArchivedClients = () => {
  return useQuery({
    queryKey: ['clients', 'archived'],
    queryFn: () => clientsApi.getArchived().then(res => res.data),
  });
};

export const useClientMutations = () => {
  const queryClient = useQueryClient();
  
  const invalidateClients = () => {
    queryClient.invalidateQueries({ queryKey: ['clients'] });
  };

  return {
    create: useMutation({
      mutationFn: clientsApi.create,
      onSuccess: invalidateClients,
    }),
    update: useMutation({
      mutationFn: ({ id, data }: { id: number; data: any }) => clientsApi.update(id, data),
      onSuccess: invalidateClients,
    }),
    archive: useMutation({
      mutationFn: clientsApi.archive,
      onSuccess: invalidateClients,
    }),
    restore: useMutation({
      mutationFn: clientsApi.restore,
      onSuccess: invalidateClients,
    }),
    delete: useMutation({
      mutationFn: clientsApi.delete,
      onSuccess: invalidateClients,
    }),
  };
};

// Reception Documents hooks
export const useReceptionDocuments = () => {
  return useQuery({
    queryKey: ['reception-documents'],
    queryFn: () => receptionDocumentsApi.getAll().then(res => res.data),
  });
};

export const useReceptionDocumentMutations = () => {
  const queryClient = useQueryClient();
  
  const invalidateReceptionDocuments = () => {
    queryClient.invalidateQueries({ queryKey: ['reception-documents'] });
    queryClient.invalidateQueries({ queryKey: ['balances'] });
  };

  return {
    create: useMutation({
      mutationFn: receptionDocumentsApi.create,
      onSuccess: invalidateReceptionDocuments,
    }),
    update: useMutation({
      mutationFn: ({ id, data }: { id: number; data: any }) => receptionDocumentsApi.update(id, data),
      onSuccess: invalidateReceptionDocuments,
    }),
    delete: useMutation({
      mutationFn: receptionDocumentsApi.delete,
      onSuccess: invalidateReceptionDocuments,
    }),
  };
};

// Shipment Documents hooks
export const useShipmentDocuments = () => {
  return useQuery({
    queryKey: ['shipment-documents'],
    queryFn: () => shipmentDocumentsApi.getAll().then(res => res.data),
  });
};

export const useShipmentDocumentMutations = () => {
  const queryClient = useQueryClient();

  const invalidate = () => {
    queryClient.invalidateQueries({ queryKey: ['shipment-documents'] });
    queryClient.invalidateQueries({ queryKey: ['balances'] });
  };

  return {
    create: useMutation({
      mutationFn: shipmentDocumentsApi.create,
      onSuccess: invalidate,
    }),
    update: useMutation({
      mutationFn: ({ id, data }: { id: number; data: any }) =>
        shipmentDocumentsApi.update(id, data),
      onSuccess: invalidate,
    }),
    sign: useMutation({
      mutationFn: (id: number) => shipmentDocumentsApi.sign(id),
      onSuccess: invalidate,
    }),
    revoke: useMutation({
      mutationFn: (id: number) => shipmentDocumentsApi.revoke(id),
      onSuccess: invalidate,
    }),
    delete: useMutation({
      mutationFn: (id: number) => shipmentDocumentsApi.delete(id),
      onSuccess: invalidate,
    }),
  };
};

// Balances hooks
export const useBalances = () => {
  return useQuery({
    queryKey: ['balances'],
    queryFn: () => balancesApi.getAll().then(res => res.data),
  });
};

