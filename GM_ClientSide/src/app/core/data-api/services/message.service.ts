import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { IndexMessageModel } from 'app/models/indexMessage.model';
import { map } from 'rxjs/operators';
const cloudinaryUrl = environment.cloudinary.url;
const cloudinaryPreset = environment.cloudinary.upload_preset;

@Injectable()
export class MessageService {
    indexMessageModel:IndexMessageModel
    constructor(private http: HttpClient) {
        // this.indexMessageModel = new IndexMessageModel();
    }
    addMessage(model: IndexMessageModel): Observable<any> {
        return this.http.post(environment.apiMessageUrl, model);
    }

    onUpload(fd: FormData) {
        fd.append('upload_preset', cloudinaryPreset)
        return this.http.post(cloudinaryUrl, fd).pipe(
            map((res: any) => {
                 this.indexMessageModel.content = res.url;
            }));
        
    }
}


