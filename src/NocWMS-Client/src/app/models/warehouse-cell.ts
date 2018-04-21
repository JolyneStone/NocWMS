
export enum CellStatus {
    available = 0,
    full = 1,
    fault = 2
};
export class WarehouseCell {
    constructor(warehouseCell?: Partial<WarehouseCell>) {
        if (warehouseCell) {
            this.id = warehouseCell.id;
            this.warehouseId = warehouseCell.warehouseId;
            this.cellName = warehouseCell.cellName;
            this.volume = warehouseCell.volume;
            this.remainderVolume = warehouseCell.remainderVolume;
            this.status = warehouseCell.status;
        }
    }

    public id: number;
    public warehouseId: number;
    public cellName: string;
    public volume: number;
    public remainderVolume: number;
    public status: CellStatus;
}
