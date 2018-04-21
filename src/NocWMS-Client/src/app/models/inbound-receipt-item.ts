export class InboundReceiptItem {
    constructor(inboundReceiptItem?: Partial<InboundReceiptItem>) {
        if (inboundReceiptItem) {
            this.id = inboundReceiptItem.id;
            this.productId = inboundReceiptItem.productId;
            this.productName = inboundReceiptItem.productName;
            this.productSpec = inboundReceiptItem.productSpec;
            this.productUnit = inboundReceiptItem.productUnit;
            this.productModel = inboundReceiptItem.productModel;
            this.productPrice = inboundReceiptItem.productPrice;
            this.categoryId = inboundReceiptItem.categoryId;
            this.categoryName = inboundReceiptItem.categoryName;
            this.num = inboundReceiptItem.num;
            this.storeCell = inboundReceiptItem.storeCell;
            this.warehouseCellId = inboundReceiptItem.warehouseCellId;
        }
    }

    public id: string;
    public productId: number;
    public productName: string;
    public categoryId: number;
    public categoryName: string;
    public num: number;
    public productUnit: string;
    public productSpec: string;
    public productModel: string;
    public productPrice: number;
    public storeCell: string;
    public warehouseCellId: number;
}