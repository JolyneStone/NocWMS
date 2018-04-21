export class OutboundReceiptItem {
    constructor(outboundReceiptItem?: Partial<OutboundReceiptItem>) {
        if (outboundReceiptItem) {
            this.id = outboundReceiptItem.id;
            this.productId = outboundReceiptItem.productId;
            this.productName = outboundReceiptItem.productName;
            this.productSpec = outboundReceiptItem.productSpec;
            this.productUnit = outboundReceiptItem.productUnit;
            this.productModel = outboundReceiptItem.productModel;
            this.productPrice = outboundReceiptItem.productPrice;
            this.categoryId = outboundReceiptItem.categoryId;
            this.categoryName = outboundReceiptItem.categoryName;
            this.num = outboundReceiptItem.num;
            this.storeCell = outboundReceiptItem.storeCell;
            this.warehouseCellId = outboundReceiptItem.warehouseCellId;
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