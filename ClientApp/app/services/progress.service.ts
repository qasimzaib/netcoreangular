import { Injectable } from '@angular/core';
import { Http, BrowserXhr } from '@angular/http';
import 'rxjs/add/operator/map';
import { Subject } from 'rxjs/Subject';

@Injectable()
export class ProgressService {
	private uploadProgress: Subject<any>;

	startTracking() {
		this.uploadProgress = new Subject();
		return this.uploadProgress;
	}

	notify(progress: any) {
		this.uploadProgress.next(progress);
	}

	endTracking() {
		this.uploadProgress.complete();
	}
}

@Injectable()
export class BrowserXhrWithProgress extends BrowserXhr {
	constructor(private service: ProgressService) { super(); }
	
	build() : XMLHttpRequest {
		var xhr: XMLHttpRequest = super.build();
		xhr.upload.onprogress = (event) => {
			this.service.notify(this.createProgress(event));
		};
		xhr.upload.onloadend = () => {
			this.service.endTracking();
		};
		return xhr;
	}

	private createProgress(event: any) {
		return {
			total: event.total,
			percentage: Math.round(event.loaded / event.total * 100)
		};
	}
}