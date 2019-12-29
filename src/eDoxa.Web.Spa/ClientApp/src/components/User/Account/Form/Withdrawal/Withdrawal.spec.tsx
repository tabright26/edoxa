import React from "react";
import { Provider } from "react-redux";
import { ReactWrapper } from "enzyme";
import Withdrawal from "./Withdrawal";
import { configureStore } from "store";
import { CURRENCY_MONEY } from "types";

const shallow = global["shallow"];
const mount = global["mount"];

const store = configureStore();

const createWrapper = (): ReactWrapper | any => {
  return mount(
    <Provider store={store}>
      <Withdrawal currency={CURRENCY_MONEY} bundles={[{}, {}, {}]} />
    </Provider>
  );
};

describe("<UserAccountWithdrawalForm />", () => {
  it("should match the snapshot", () => {
    const shallowWrapper = shallow(<Withdrawal />);
    expect(shallowWrapper).toMatchSnapshot();
  });

  describe("defines account withdrawal form fields", () => {
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
