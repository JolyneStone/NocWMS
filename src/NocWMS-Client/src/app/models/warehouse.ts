import { WarehouseCell } from './warehouse-cell';
export class Warehouse {
    constructor(warehouse?: Partial<Warehouse>) {
        if (warehouse) {
            this.id = warehouse.id;
            this.warehouseName = warehouse.warehouseName;
            this.managerName = warehouse.managerName;
            this.staffId = warehouse.staffId;
            this.province = warehouse.province;
            this.address = warehouse.address;
            this.cells = warehouse.cells;
        }
    }

    public id: number;
    public warehouseName: string;
    public staffId: string;
    public managerName: string;
    public province: string;
    public address: string;
    public cells: WarehouseCell[];
}