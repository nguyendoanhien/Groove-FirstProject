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

    }

    transform(message: string) {
        var res = this._appHelperService.ExtractUrl(message);
        var result: Promise<any[]>;
        if (res !== null) {
            result = Promise.all(res.map(async (val): Promise<any> => {
                var obj = {
                    imgLink: await this.getOgImage(val),
                    urlLink: val
                }
                return obj;
            }))
        }
        console.log('new res');
        console.log(result);
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
            imageUrl = response.image[0].url;
            console.log(imageUrl)
        }
        );
        return imageUrl;
    }
}
