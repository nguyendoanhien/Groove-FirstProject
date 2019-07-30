import { Pipe, PipeTransform } from '@angular/core';
import { FuseUtils } from '@fuse/utils';
import { FacebookService } from 'ngx-facebook';
import { AppHelperService } from 'app/core/utilities/app-helper.service';
import { ApiMethod } from 'ngx-facebook/dist/esm/providers/facebook';

@Pipe({ name: 'extractUrl' })
export class ExtractUrlPipe implements PipeTransform {
    /**
     * Transform
     *
     * @param {any[]} mainArr
     * @param {string} searchText
     * @param {string} property
     * @returns {any}
     */
    constructor(
        public _appHelperService: AppHelperService,
        private fbk: FacebookService) {
        this.fbk.init({
            appId: '354060818601401',
            // autoLogAppEvents: true,
            xfbml: true,
            version: 'v3.3'
        });

    }

    transform(message: string) {

        var res = this._appHelperService.ExtractUrl(message);

        var result: Promise<any[]>;
        if (res != null) {
            result = Promise.all(res.map(async (val): Promise<any> => {
                var imageLink = await this.getOgImage(val);
                if (imageLink == "") imageLink = val;
                var obj = {
                    imgLink: imageLink,
                    urlLink: val
                }
                return obj;
            })
            )


        }
        return result;

    }
    async getOgImage(urlPath: string) {

        let imageUrl = '';
        var apiMethod: ApiMethod = "post";
        await this.fbk.api(
            '/',
            apiMethod,
            { "scrape": "true", "id": urlPath }
        ).then(function (response) {
            console.log(response);
            imageUrl = response.image[0].url;
        }
        ).catch(err => console.log('promise eror is' + err));

        return imageUrl;
    }
}
