import React from "react";
import { Provider } from "react-redux";
import { ReactWrapper } from "enzyme";
import Forgot from "./Forgot";
import { configureStore } from "store";
import Input from "components/Shared/Input";
import { EMAIL_REQUIRED, EMAIL_INVALID } from "validation";

const shallow = global["shallow"];
const mount = global["mount"];

const initialState: any = {};
const store = configureStore(initialState);

const createWrapper = (): ReactWrapper | any => {
  return mount(
    <Provider store={store}>
      <Forgot />
    </Provider>
  );
};

describe("<UserPasswordForgotForm />", () => {
  it("should match the snapshot", () => {
    const shallowWrapper = shallow(<Forgot />);
    expect(shallowWrapper).toMatchSnapshot();
  });

  describe("defines password forgot form fields", () => {
    it("renders email field", () => {
      const wrapper = createWrapper();
      const field = wrapper.findFieldByName("email");

      expect(field.prop("type")).toBe("text");
      expect(field.prop("label")).toBe("Email");
      expect(field.prop("component")).toBe(Input.Text);
    });

    it("renders submit button", () => {
      const wrapper = createWrapper();
      const submitButton = wrapper.findSubmitButton();

      expect(submitButton.prop("type")).toBe("submit");
      expect(submitButton.text()).toBe("Send Email");
    });
  });

  describe("form validation", () => {
    describe("email validation", () => {
      it("shows error when email is set to blank", () => {
        const wrapper = createWrapper();
        const input = wrapper.findInputByName("email");
        input.simulate("blur");

        const errorPresent = wrapper.findFormFeedback(EMAIL_REQUIRED);
        expect(errorPresent).toBeTruthy();
      });

      it("shows error when email is set to invalid", () => {
        const wrapper = createWrapper();
        const input = wrapper.findInputByName("email");
        input.simulate("change", { target: { value: "invalid" } });

        const errorPresent = wrapper.findFormFeedback(EMAIL_INVALID);
        expect(errorPresent).toBeTruthy();
      });
    });
  });
});
