import { ToastrService } from 'ngx-toastr';
import { InboundReceipt } from './../../../models/inbound-receipt';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { InboundReceiptService } from '../../../services/inbound-receipt/inbound-receipt.service';

@Component({
  selector: 'noc-inbound-receipt-detail',
  templateUrl: './inbound-receipt-detail.component.html',
  styleUrls: ['./inbound-receipt-detail.component.css']
})
export class InboundReceiptDetailComponent implements OnInit {
  public inboundReceipt: InboundReceipt;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private inboundReceiptService: InboundReceiptService,
    private toastrService: ToastrService,
  ) { }

  ngOnInit() {
    this.activatedRoute.queryParams
      .subscribe(params => {
        let id = params.id;
        this.inboundReceiptService.getInboundReceipt(id)
          .subscribe(data => {
            if (data.isSuccess) {
              this.inboundReceipt = data.data as InboundReceipt;
            }
          })
      })
  }

  public onChange() {
    this.inboundReceiptService.updateInboundReceipt(this.inboundReceipt)
      .subscribe(data => {
        if (data.isSuccess) {
          this.toastrService.success("更新成功");
        } else {
          this.toastrService.error("更新失败");
        }
      })
  }
  public returnToTable() {
    this.router.navigateByUrl('/workspace/inventory/inbound-receipt-table');
  }

  public printReceipt() {
    (<any>window).print();
  }
}