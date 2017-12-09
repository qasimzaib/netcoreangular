import { ErrorHandler } from "@angular/core";

export class AppErrorHandler implements ErrorHandler {
	handleError(error: any): void {
		console.log("Error");
		// this.toastyService.error({
		// 	title: 'Error',
		// 	msg: 'An unexpected error occured',
		// 	theme: 'bootstrap',
		// 	showClose: true,
		// 	timeout: 5000
		// });
	}
}