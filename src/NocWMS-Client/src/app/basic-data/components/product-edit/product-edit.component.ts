import { Component, OnInit } from '@angular/core';
import { Product } from '../../../models/product';
import { Router, ActivatedRoute } from '@angular/router';
import { ProductService } from '../../../services/product/product.service';
import { ToastrService } from 'ngx-toastr';
import { UserService } from './../../../services/user/user.service';

@Component({
  selector: 'noc-product-edit',
  templateUrl: './product-edit.component.html',
  styleUrls: ['./product-edit.component.css']
})
export class ProductEditComponent implements OnInit {

  public canEdit: boolean;
  public product: Product = new Product();
  constructor(
    private userService: UserService,
    private productService: ProductService,
    private toastrService: ToastrService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit() {
    this.canEdit = this.userService.isAdmin();
    this.activatedRoute.queryParams
      .subscribe(params => {
        this.product.categoryId = params.id;
        let productId = params.productId;
        this.productService.get(productId)
          .subscribe(data => {
            if (data.isSuccess) {
              this.product = data.data as Product;
            } else {
              this.toastrService.info("无法得到该产品信息", "提示");
            }
          });
      });
  }

  public onSubmit() {
    this.productService.update(this.product)
      .subscribe(data => {
        if (data.isSuccess) {
          this.toastrService.success("更新产品数据成功", "成功");
          this.product = data.data as Product;
        } else {
          this.toastrService.error("更新产品数据失败: " + data.message, "失败");
        }
      });
  }

  public backToTable() {
    this.router.navigate(['../product-table'], {
      relativeTo: this.activatedRoute, queryParams: {
        'id': this.product.categoryId
      }
    });
  }
}
