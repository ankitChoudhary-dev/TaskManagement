export enum TaskStatus {
  Pending = 'Pending',
  InProgress = 'In Progress',
  Completed = 'Completed'
}

export enum TaskPriority {
  Low = 'Low',
  Medium = 'Medium',
  High = 'High'
}

export interface Task {
  id: number;
  title: string;
  description?: string;
  status: string;
  priority?: string;
  dueDate?: string | Date;
  projectId: number;
  projectName?: string;
  assignedTo: number;            
  assignedUserName?: string;     
  createdBy?: number;
  createdOn: string | Date;
}

export interface CreateTask {
  title: string;
  description?: string;
  status: string;
  priority?: string;
  dueDate?: string | Date;
  projectId: number;
  assignedTo: number;            
}

export interface UpdateTask extends CreateTask {
  id: number;
}