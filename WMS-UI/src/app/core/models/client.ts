export interface Client {
  clientId: number;
  clientName: string;
  clientAddress?: string;
  clientPhoneNumber?: number;
  clientLocation?: string;
  status: boolean;
  createdOn: string;
}