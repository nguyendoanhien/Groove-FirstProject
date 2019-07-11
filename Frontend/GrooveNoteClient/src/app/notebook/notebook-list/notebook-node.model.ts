export class NotebookNodeModel {

    constructor(public Id: number, public Title: string, public Type: NodeType) {
    }
}

export enum NodeType {
    Notebook = 1,
    Note = 2
}