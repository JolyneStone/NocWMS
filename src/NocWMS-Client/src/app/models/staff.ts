import { Gender } from "./gender";

export class Staff {
    constructor(staff?: Partial<Staff>) {
        if (staff) {
            this.id = staff.id;
            this.staffName = staff.staffName;
            this.gender = staff.gender;
            this.duty = staff.duty;
            this.email = staff.email;
            this.qqNumber = staff.qqNumber;
            this.createTime = staff.createTime;
        }
    }

    public id: string;
    public staffName: string;
    public gender: Gender;
    public duty: string;
    public telephone: string;
    public qqNumber: string;
    public email: string;
    public remarks: string;
    public createTime: Date;
}