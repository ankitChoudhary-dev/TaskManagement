export interface CreateProject {
  name: string;
  description?: string;
  startDate?: Date;
  endDate?: Date;
  status: string;
}