import { Pipe, PipeTransform } from '@angular/core';
import { Gender } from '../models/gender';

@Pipe({
  name: 'gender'
})
export class GenderPipe implements PipeTransform {

  transform(value: Gender | number | string, args?: any): any {
    if (typeof value === "string") {
      const gender = value as string;
      return gender === 'male' ? '男' : '女';
    } else if (typeof value === "number") {
      const gender = value as number;
      return gender === 0 ? '男' : '女';
    } else {
      const gender = value as Gender;
      return gender === Gender.male ? '男' : '女';
    }
  }
}
