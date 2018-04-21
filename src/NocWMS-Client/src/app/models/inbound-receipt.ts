import { InboundReceiptItem } from "./inbound-receipt-item";

export class InboundReceipt {
    constructor(inboundReceipt?: Partial<InboundReceipt>) {
        if (inboundReceipt) {
            this.id = inboundReceipt.id;
            this.staffId = inboundReceipt.staffId;
            this.warehouseId = inboundReceipt.warehouseId;
            this.warehouseName = inboundReceipt.warehouseName;
            this.vendorId = inboundReceipt.vendorId;
            this.vendorName = inboundReceipt.vendorName;
            this.handlerName = inboundReceipt.handlerName;
            this.acceptor = inboundReceipt.acceptor;
            this.deliveryman = inboundReceipt.deliveryman
            this.waybillNo = inboundReceipt.waybillNo;
            this.isDone = inboundReceipt.isDone;
            this.createTime = inboundReceipt.createTime;
            this.inboundReceiptItems = inboundReceipt.inboundReceiptItems;
        }
    }

    public id: string;
    public staffId: string;
    public warehouseId: number;
    public warehouseName: string;
    public vendorId: string;
    public vendorName: string;
    public handlerName: string;
    public acceptor: string;
    public deliveryman: string;
    public waybillNo: string;
    public isDone: boolean;
    public createTime: string;
    public inboundReceiptItems: InboundReceiptItem[];
    public get total(): number {
        let sum = 0;
        if (this.inboundReceiptItems) {
            this.inboundReceiptItems.forEach((v) => sum += v.productPrice * v.num);
        }

        return sum;
    }
}