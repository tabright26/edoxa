import React from "react";
import { Provider } from "react-redux";
import { ReactWrapper } from "enzyme";
import Deposit from "./Create";
import { configureStore } from "store";
import { CURRENCY_MONEY, TRANSACTION_TYPE_DEPOSIT } from "types";

const shallow = global["shallow"];
const mount = global["mount"];

const store = configureStore();

const createWrapper = (): ReactWrapper | any => {
  return mount(
    <Provider store={store}>
      <Deposit
        transactionType={TRANSACTION_TYPE_DEPOSIT}
        currency={CURRENCY_MONEY}
        handleCancel={() => {}}
      />
    </Provider>
  );
};

describe("<UserAccountDepositForm />", () => {
  it("should match the snapshot", () => {
    const shallowWrapper = shallow(
      <Provider store={store}>
        <Deposit
          handleCancel={() => {}}
          transactionType={TRANSACTION_TYPE_DEPOSIT}
          currency={CURRENCY_MONEY}
        />
      </Provider>
    );
    expect(shallowWrapper).toMatchSnapshot();
  });

  describe("defines account deposit form fields", () => {
    it("renders transactionBundleId field", () => {
      const wrapper = createWrapper();
      const field = wrapper.findFieldByName("transactionBundleId");

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
