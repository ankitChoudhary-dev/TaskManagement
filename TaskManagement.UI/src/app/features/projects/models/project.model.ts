export interface Project {
  id: number;
  name: string;
  description?: string;
  startDate?: string | Date; 
  endDate?: string | Date;   
  status: string;
  createdBy: number;
  createdByName: string;
  createdOn: string | Date;  
}