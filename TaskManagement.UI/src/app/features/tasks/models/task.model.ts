export interface Task {
  id: number;
  title: string;
  description?: string;
  status: string;
  priority?: string;
  dueDate?: string | Date;
  projectId: number;
  projectName?: string;
  assignedToUserId?: number;
  assignedToUserName?: string;
  createdOn: string | Date;
}

export interface CreateTask {
  title: string;
  description?: string;
  status: string;
  priority?: string;
  dueDate?: string | Date;
  projectId: number;
  assignedToUserId?: number;
}