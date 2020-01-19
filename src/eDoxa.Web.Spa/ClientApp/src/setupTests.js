import { configure, mount, shallow, render } from "enzyme";
import Adapter from "enzyme-adapter-react-16";
import "utils/test/helpers";

global.mount = mount;
global.shallow = shallow;
global.render = render;

configure({ adapter: new Adapter() });

if (global.document) {
  document.createRange = () => ({
    setStart: () => {},
    setEnd: () => {},
    commonAncestorContainer: {
      nodeName: "BODY",
      ownerDocument: document
    }
  });
}
