import React from "react";
import { Provider } from "react-redux";
import { ReactWrapper } from "enzyme";
import Update from "./Update";
import { configureStore } from "store";
import Input from "components/Shared/Input";
import {
  CC_MONTH_REQUIRED,
  CC_MONTH_INVALID,
  CC_YEAR_REQUIRED,
  CC_YEAR_INVALID
} from "validation";

const shallow = global["shallow"];
const mount = global["mount"];

const initialState: any = {};
const store = configureStore(initialState);

const createWrapper = (): ReactWrapper | any => {
  const initialValues = {
    card: {
      brand: "visa",
      last4: "1234",
      expYear: "2030"
    }
  };
  return mount(
    <Provider store={store}>
      <Update intialValues={initialValues} />
    </Provider>
  );
};

describe("<PaymentMethodUpdateForm />", () => {
  it("should match the snapshot", () => {
    const shallowWrapper = shallow(<Update />);
    expect(shallowWrapper).toMatchSnapshot();
  });

  describe("defines paymentMethod update form fields", () => {
    it("renders exp_month field", () => {
      const wrapper = createWrapper();
      const field = wrapper.findFieldByName("exp_month");

      expect(field.prop("type")).toBe("select");
      expect(field.prop("component")).toBe(Input.Select);
    });

    it("renders exp_year field", () => {
      const wrapper = createWrapper();
      const field = wrapper.findFieldByName("exp_year");

      expect(field.prop("type")).toBe("select");
      expect(field.prop("component")).toBe(Input.Select);
    });

    it("renders save button", () => {
      const wrapper = createWrapper();
      const saveButton = wrapper.find("SaveButton").first();
      const button = saveButton.find("button").first();

      expect(button.prop("type")).toBe("submit");
      expect(button.text()).toBe("Save");
    });

    it("renders cancel button", () => {
      const wrapper = createWrapper();
      const cancelButton = wrapper.find("CancelButton").first();
      const button = cancelButton.find("button").first();

      expect(button.prop("type")).toBe("button");
      expect(button.text()).toBe("Cancel");
    });
  });

  describe("form validation", () => {
    describe("exp_month validation", () => {
      it("shows error when exp_month is set to blank", () => {
        const wrapper = createWrapper();
        const input = wrapper.findInputByName("exp_month");
        input.simulate("blur");

        const errorPresent = wrapper.findFormFeedback(CC_MONTH_REQUIRED);
        expect(errorPresent).toBeTruthy();
      });

      it("shows error when name is set to invalid", () => {
        const wrapper = createWrapper();
        const input = wrapper.findInputByName("exp_month");
        input.simulate("change", { target: { value: "11" } });

        const errorPresent = wrapper.findFormFeedback(CC_MONTH_INVALID);
        expect(errorPresent).toBeTruthy();
      });
    });
    describe("exp_year validation", () => {
      it("shows error when exp_year is set to blank", () => {
        const wrapper = createWrapper();
        const input = wrapper.findInputByName("exp_year");
        input.simulate("blur");

        const errorPresent = wrapper.findFormFeedback(CC_YEAR_REQUIRED);
        expect(errorPresent).toBeTruthy();
      });

      it("shows error when name is set to invalid", () => {
        const wrapper = createWrapper();
        const input = wrapper.findInputByName("exp_year");
        input.simulate("change", { target: { value: "2021" } });

        const errorPresent = wrapper.findFormFeedback(CC_YEAR_INVALID);
        expect(errorPresent).toBeTruthy();
      });
    });
  });
});
