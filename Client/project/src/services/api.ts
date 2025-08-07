import axios from 'axios';
import {
  Resource,
  Measurement,
  Client,
  ReceptionDocument,
  ShipmentDocument,
  Balance,
  ResourceFormData,
  MeasurementFormData,
  ClientFormData,
  ReceptionDocumentFormData,
  ShipmentDocumentFormData,
} from '../types';

const api = axios.create({
  baseURL: 'https://localhost:7018',
  headers: {
    'Content-Type': 'application/json',
  },
});

// Resources API
export const resourcesApi = {
  getActive: () => api.get<Resource[]>('/api/resources/active'),
  getArchived: () => api.get<Resource[]>('/api/resources/archived'),
  getById: (id: number) => api.get<Resource>(`/api/resources/${id}`),
  create: (data: ResourceFormData) => api.post<Resource>('/api/resources', data),
  update: (id: number, data: ResourceFormData) =>
    api.put(`/api/resources/${id}`, { ...data, isArchived: data.isArchived }),
  archive: (id: number, data: ResourceFormData) => 
    api.put(`/api/resources/${id}`, { ...data, isArchived: true }),
  restore: (id: number, data: ResourceFormData) =>
    api.put(`/api/resources/${id}`, { ...data, isArchived: false }),
  delete: (id: number) => api.delete(`/api/resources/${id}`),
};

// Measurements API
export const measurementsApi = {
  getActive: () => api.get<Measurement[]>('/api/measurements/active'),
  getArchived: () => api.get<Measurement[]>('/api/measurements/archived'),
  getById: (id: number) => api.get<Measurement>(`/api/measurements/${id}`),
  create: (data: MeasurementFormData) => api.post<Measurement>('/api/measurements', data),
  update: (id: number, data: MeasurementFormData) =>
    api.put(`/api/measurements/${id}`, { ...data, isArchived: data.isArchived }),
  archive: (id: number, data: MeasurementFormData) =>
    api.put(`/api/measurements/${id}`, { ...data, isArchived: true }),
  restore: (id: number, data: MeasurementFormData) =>
    api.put(`/api/measurements/${id}`, { ...data, isArchived: false }),
  delete: (id: number) => api.delete(`/api/measurements/${id}`),
};

// Clients API
export const clientsApi = {
  getActive: () => api.get<Client[]>('/api/clients/active'),
  getArchived: () => api.get<Client[]>('/api/clients/archived'),
  getById: (id: number) => api.get<Client>(`/api/clients/${id}`),
  create: (data: ClientFormData) => api.post<Client>('/api/clients', data),
  update: (id: number, data: ClientFormData) =>
    api.put(`/api/clients/${id}`, { ...data, isArchived: data.isArchived }),
  archive: (id: number, data: ClientFormData) =>
    api.put(`/api/clients/${id}`, { ...data, isArchived: true }),
  restore: (id: number, data: ClientFormData) =>
    api.put(`/api/clients/${id}`, { ...data, isArchived: false }),
  delete: (id: number) => api.delete(`/api/clients/${id}`),
};


// Reception Documents API
export const receptionDocumentsApi = {
  getAll: () => api.get<ReceptionDocument[]>('/api/receptions'),
  getById: (id: number) => api.get<ReceptionDocument>(`/api/receptions/${id}`),
  create: (data: ReceptionDocumentFormData) => api.post<ReceptionDocument>('/api/receptions', data),
  update: (id: number, data: ReceptionDocumentFormData) => api.put<ReceptionDocument>(`/api/receptions/${id}`, { ...data, id }),
  delete: (id: number) => api.delete(`/api/receptions/${id}`),
};


// Shipment Documents API
export const shipmentDocumentsApi = {
  getAll: () => api.get<ShipmentDocument[]>('/api/shipments'),
  getById: (id: number) => api.get<ShipmentDocument>(`/api/shipments/${id}`),
  create: (data: ShipmentDocumentFormData) => api.post<ShipmentDocument>('/api/shipments', data),
  update: (id: number, data: ShipmentDocumentFormData) =>
  api.put<ShipmentDocument>(`/api/shipments`, { ...data, id }),
  sign: (id: number) => api.patch(`/api/shipments/${id}/status/1`),
revoke: (id: number) => api.patch(`/api/shipments/${id}/status/2`),
  delete: (id: number) => api.delete(`/api/shipments/${id}`),
};

// Balances API
export const balancesApi = {
  getAll: () => api.get<Balance[]>('/api/balances'),
};

export default api;