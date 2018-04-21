import { OutboundReceiptItem } from "./outbound-receipt-item";

export class OutboundReceipt {
    constructor(outboundReceipt?: Partial<OutboundReceipt>) {
        if (outboundReceipt) {
            this.id = outboundReceipt.id;
            this.staffId = outboundReceipt.staffId;
            this.warehouseId = outboundReceipt.warehouseId;
            this.warehouseName = outboundReceipt.warehouseName;
            this.customerId = outboundReceipt.customerId;
            this.customerName = outboundReceipt.customerName;
            this.handlerName = outboundReceipt.handlerName;
            this.acceptor = outboundReceipt.acceptor;
            this.deliveryman = outboundReceipt.deliveryman
            this.waybillNo = outboundReceipt.waybillNo;
            this.isDone = outboundReceipt.isDone;
            this.createTime = outboundReceipt.createTime;
            this.outboundReceiptItems = outboundReceipt.outboundReceiptItems;
        }
    }

    public id: string;
    public staffId: string;
    public warehouseId: number;
    public warehouseName: string;
    public customerId: string;
    public customerName: string;
    public handlerName: string;
    public acceptor: string;
    public deliveryman: string;
    public waybillNo: string;
    public isDone: boolean;
    public createTime: string;
    public outboundReceiptItems: OutboundReceiptItem[];
    public get total(): number {
        let sum = 0;
        if (this.outboundReceiptItems) {
            this.outboundReceiptItems.forEach((v) => sum += v.productPrice * v.num);
        }

        return sum;
    }
}