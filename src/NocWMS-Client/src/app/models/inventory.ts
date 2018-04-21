import { InventoryCell } from "./inventory-cell";

export class Inventory {
    constructor(inventory?: Partial<Inventory>) {
        if (inventory) {
            this.id = inventory.id;
            this.warehouseId = inventory.warehouseId;
            this.warehouseName = inventory.warehouseName;
            this.categoryId = inventory.categoryId;
            this.categoryName = inventory.categoryName;
            this.productId = inventory.productId;
            this.productName = inventory.productName;
            this.productSpec = inventory.productSpec;
            this.productModel = inventory.productModel;
            this.productUnit = inventory.productUnit;
            this.productPrice = inventory.productPrice;
            this.realInventory = inventory.realInventory;
            this.bookInventory = inventory.bookInventory;
            this.inventoryCells = inventory.inventoryCells;
        }
    }

    public id: number;
    public warehouseId: number;
    public warehouseName: string;
    public categoryId: number;
    public categoryName: string;
    public productId: number;
    public productName: string;
    public productUnit: string;
    public productModel: string;
    public productSpec: string;
    public productPrice: number;
    public bookInventory: number;
    public realInventory: number;
    public get diffInventory(): number {
        return this.bookInventory - this.realInventory;
    }
    public get totalPrice(): number {
        return this.productPrice * this.bookInventory;
    }

    public inventoryCells: InventoryCell[];
}