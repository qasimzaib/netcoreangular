import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { VehicleService } from '../../services/vehicle.service';
import { ToastyService } from 'ng2-toasty';
import { PhotoService } from '../../services/photo.service';

@Component({
	selector: 'app-vehicle-details',
	templateUrl: './vehicle-details.component.html',
	styleUrls: ['./vehicle-details.component.css']
})
export class VehicleDetailsComponent implements OnInit {
	@ViewChild('fileInput') fileInput: ElementRef;
	vehicle: any;
	vehicleId: number;

	constructor(
		private route: ActivatedRoute,
		private router: Router,
		private vehicleService: VehicleService,
		private photoService: PhotoService,
		private toastyService: ToastyService
	) {
		route.params.subscribe(p => {
			this.vehicleId = +p['id'];
			if (isNaN(this.vehicleId) || this.vehicleId <= 0) {
				router.navigate(['vehicles']);
				return;
			}
		});
	}

	ngOnInit() {
		this.vehicleService
			.getVehicle(this.vehicleId)
			.subscribe(
				v => this.vehicle = v,
				err => {
					if (err.status == 404) {
						this.router.navigate(['/vehicles']);
						return;
					}
				}
			);
	}

	delete() {
		if (confirm("Are you sure?")) {
			this.vehicleService
				.delete(this.vehicle.id)
				.subscribe(
					x => {
						this.router.navigate(['/vehicles']);
					}
				);
		}
	}

	uploadPhoto() {
		var nativeElement: HTMLInputElement = this.fileInput.nativeElement;
		this.photoService.upload(this.vehicleId, nativeElement.files![0])
			.subscribe(
				x => console.log(x)
			);
	}
}