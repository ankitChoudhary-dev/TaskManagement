export interface Task {

    id:number;

    projectId:number;

    title:string;

    description:string;

    assignedUser:string;

    priority:string;

    status:string;

    dueDate:Date;

}