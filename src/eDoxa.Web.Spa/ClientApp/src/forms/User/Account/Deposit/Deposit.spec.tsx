import React from "react";
import { Provider } from "react-redux";
import { ReactWrapper } from "enzyme";
import Deposit from "./Deposit";
import { configureStore } from "store";
import { MONEY } from "types";

const shallow = global["shallow"];
const mount = global["mount"];

const initialState: any = {};
const store = configureStore(initialState);

const createWrapper = (): ReactWrapper | any => {
  return mount(
    <Provider store={store}>
      <Deposit currency={MONEY} bundles={[{}, {}, {}]} />
    </Provider>
  );
};

describe("<UserAccountDepositForm />", () => {
  it("should match the snapshot", () => {
    const shallowWrapper = shallow(<Deposit />);
    expect(shallowWrapper).toMatchSnapshot();
  });

  describe("defines account deposit form fields", () => {
    it("renders bundle field", () => {
      const wrapper = createWrapper();
      const field = wrapper.findFieldByName("bundle");

      expect(field.prop("type")).toBe("radio");
    });

    it("renders save button", () => {
      const wrapper = createWrapper();
      const saveButton = wrapper.findSaveButton();

      expect(saveButton.prop("type")).toBe("submit");
      expect(saveButton.text()).toBe("Save");
    });

    it("renders cancel button", () => {
      const wrapper = createWrapper();
      const cancelButton = wrapper.findCancelButton();

      expect(cancelButton.prop("type")).toBe("button");
      expect(cancelButton.text()).toBe("Cancel");
    });
  });
});
