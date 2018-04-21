import { ReceiptType } from "./ReceiptType";

export class InventoryCell {
    constructor(inventoryCell?: Partial<InventoryCell>) {
        if (inventoryCell) {
            this.id = inventoryCell.id;
            this.inventoryId = inventoryCell.inventoryId;
            this.warehouseCellId = inventoryCell.warehouseCellId;
            this.cellName = inventoryCell.cellName;
            this.receiptType = inventoryCell.receiptType;
            this.receiptId = inventoryCell.receiptId;
            this.num = inventoryCell.num;
            this.updateTime = inventoryCell.updateTime;
        }
    }
    public id: number;
    public inventoryId: number;
    public warehouseCellId: number;
    public cellName: string;
    public receiptType: ReceiptType;
    public receiptId: string;
    public updateTime: string;
    public num: number;
}