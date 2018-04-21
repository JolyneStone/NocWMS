import { Gender } from './gender';
export class Vendor {
    constructor(vendor?: Partial<Vendor>) {
        if (vendor) {
            this.id = vendor.id;
            this.vendorName = vendor.vendorName;
            this.gender = vendor.gender;
            this.address = vendor.address;
            this.contact = vendor.contact;
            this.duty = vendor.duty;
            this.fax = vendor.fax;
            this.email = vendor.email;
            this.postCode = vendor.postCode;
            this.createTime = vendor.createTime;
        }
    }

    public id: string;
    public vendorName: string;
    public gender: Gender;
    public address: string;
    public contact: string;
    public duty: string;
    public telephone: string;
    public fax: string;
    public postCode: string;
    public email: string;
    public remarks: string;
    public createTime: Date;
}