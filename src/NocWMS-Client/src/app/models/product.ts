export class Product {
    constructor(product?: Partial<Product>) {
        if (product) {
            this.id = product.id;
            this.categoryId = product.categoryId;
            this.productName = product.productName;
            this.sellPrice = product.sellPrice;
            this.spec = product.spec;
            this.unit = product.unit;
            this.model = product.model;
        }
    }

    public id: number;
    public categoryId: number;
    public productName: string;
    public unit: string;
    public spec: string;
    public model: string;
    public sellPrice: number;
}