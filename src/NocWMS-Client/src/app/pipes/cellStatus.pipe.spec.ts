import { CellStatusPipe } from './cellStatus.pipe';

describe('CellPipe', () => {
  it('create an instance', () => {
    const pipe = new CellStatusPipe();
    expect(pipe).toBeTruthy();
  });
});
