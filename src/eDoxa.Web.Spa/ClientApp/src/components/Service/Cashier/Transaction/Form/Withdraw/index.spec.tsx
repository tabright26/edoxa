import React from "react";
import { Provider } from "react-redux";
import { ReactWrapper } from "enzyme";
import Deposit from ".";
import store from "store";
import {
  StaticOptionsActions,
  LOAD_CASHIER_STATIC_OPTIONS_SUCCESS
} from "store/actions/static/types";
import {
  findFieldByName,
  findSubmitButton,
  findCancelButton
} from "test/helper";
import { TRANSACTION_TYPE_DEPOSIT, CURRENCY_TYPE_MONEY } from "types/cashier";

const shallow = global["shallow"];
const mount = global["mount"];

const action: StaticOptionsActions = {
  type: LOAD_CASHIER_STATIC_OPTIONS_SUCCESS,
  error: null,
  meta: null,
  payload: {
    status: 200,
    statusText: "Ok",
    headers: {},
    config: {},
    data: {
      transaction: {
        bundles: [
          {
            id: 1,
            type: TRANSACTION_TYPE_DEPOSIT,
            currency: {
              amount: 10,
              type: CURRENCY_TYPE_MONEY
            },
            price: {
              amount: 10,
              type: CURRENCY_TYPE_MONEY
            },
            description: null,
            notes: null,
            disabled: false,
            deprecated: false
          }
        ]
      },
      promotion: {}
    }
  }
};

store.dispatch<any>(action);

const createWrapper = (): ReactWrapper => {
  return mount(
    <Provider store={store}>
      <Deposit currencyType={CURRENCY_TYPE_MONEY} handleCancel={() => {}} />
    </Provider>
  );
};

describe("<UserAccountDepositForm />", () => {
  it("should match the snapshot", () => {
    const shallowWrapper = shallow(
      <Provider store={store}>
        <Deposit handleCancel={() => {}} currencyType={CURRENCY_TYPE_MONEY} />
      </Provider>
    );
    expect(shallowWrapper).toMatchSnapshot();
  });

  describe("defines account deposit form fields", () => {
    it("renders transactionBundleId field", () => {
      const wrapper = createWrapper();
      const field = findFieldByName(wrapper, "bundleId");

      expect(field.prop("type")).toBe("radio");
    });

    it("renders submit button", () => {
      const wrapper = createWrapper();
      const submitButton = findSubmitButton(wrapper);

      expect(submitButton.prop("type")).toBe("submit");
      expect(submitButton.text()).toBe("Confirm");
    });

    it("renders cancel button", () => {
      const wrapper = createWrapper();
      const cancelButton = findCancelButton(wrapper);

      expect(cancelButton.prop("type")).toBe("button");
      expect(cancelButton.text()).toBe("Cancel");
    });
  });
});
