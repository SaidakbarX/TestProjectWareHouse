// Entity Types
export interface Resource {
  id: number;
  name: string;
  description?: string;
  isArchived: boolean;
  createdAt: string;
  updatedAt: string;
}

export interface Measurement {
  id: number;
  name: string;
  symbol: string;
  isArchived: boolean;
  createdAt: string;
  updatedAt: string;
}

export interface Client {
  id: number;
  name: string;
  address?: string;
  isArchived: boolean;
}

export interface ReceptionDocument {
  id: number;
  number: string;        // documentNumber → number
  date: string;          // documentDate → date
  items: ReceptionItem[];
}


export interface ReceptionItem {
  id: number;
  receptionDocumentId: number;
  resourceId: number;
  resourceName?: string;
  measurementId: number;
  measurementName?: string;
  measurementSymbol?: string;
  quantity: number;
  unitPrice: number;
  totalPrice: number;
}

export interface ShipmentDocument {
  id: number;
  status: number;
  clientId: number;
  clientName?: string;
  number: string;       
  date: string;         
  isSigned: boolean;
  items: ShipmentItem[];
}


export interface ShipmentItem {
  id: number;
  resourceId: number;
  resourceName?: string;
  measurementId: number;
  measurementName?: string;
  quantity: number;
}

export interface Balance {
  resourceId: number;
  resourceName: string;
  measurementId: number;
  measurementName: string;
  quantity: number;
}

// Form Types
export interface ResourceFormData {
  name: string;
  isArchived: boolean;
}

export interface MeasurementFormData {
  name: string;
  isArchived:boolean;
}

export interface ClientFormData {
  name: string;
  address?: string;
  isArchived: boolean;
}

export interface ReceptionDocumentFormData {
  number: string;        // documentNumber → number
  date: string;          // documentDate → date
  items: Omit<ReceptionItem, 'id' | 'receptionDocumentId' | 'resourceName' | 'measurementName' | 'measurementSymbol'>[];
}


export interface ShipmentDocumentFormData {
  number: string;
  date: string;
  clientId: number;
  status: number;
  items: ShipmentDocumentItemFormData[];
}

export interface ShipmentDocumentItemFormData {
  resourceId: number;
  measurementId: number;
  quantity: number;
}

// API Response Types
export interface ApiResponse<T> {
  data: T;
  success: boolean;
  message?: string;
}

export interface PaginatedResponse<T> {
  data: T[];
  total: number;
  page: number;
  limit: number;
}