import { Gender } from './gender';

export class Customer {
    constructor(customer?: Partial<Customer>) {
        if (customer) {
            this.id = customer.id;
            this.customerName = customer.customerName;
            this.contact = customer.contact;
            this.duty = customer.duty;
            this.gender = customer.gender;
            this.telephone = customer.telephone;
            this.address = customer.address;
            this.remarks = customer.remarks;
            this.postCode = customer.postCode;
            this.fax = customer.fax;
            this.email = customer.email;
            this.createTime = customer.createTime;
        }
    }

    public id: string;
    public customerName: string;
    public address: string;
    public contact: string;
    public gender: Gender;
    public duty: string;
    public telephone: string;
    public postCode: string;
    public fax: string;
    public email: string;
    public remarks: string;
    public createTime: Date;
}
