
export class Category {
    constructor(category?: Partial<Category>) {
        if (category) {
            this.id = category.id;
            this.categoryName = category.categoryName;
            this.creator = category.creator;
            this.createTime = category.createTime;
            this.remarks = category.remarks;
        }
    }

    public id: number;
    public categoryName: string;
    public creator: string;
    public createTime: string;
    public remarks: string;
}