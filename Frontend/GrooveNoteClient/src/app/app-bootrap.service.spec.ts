import { TestBed } from '@angular/core/testing';

import { AppBootrapService} from './app-bootrap.service';

describe('AppBootrapServiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AppBootrapService = TestBed.get(AppBootrapService);
    expect(service).toBeTruthy();
  });
});
