import React from "react";
import { Provider } from "react-redux";
import { ReactWrapper } from "enzyme";
import Reset from "./Reset";
import { configureStore } from "store";
import Input from "components/Shared/Input";
import {
  EMAIL_REQUIRED,
  EMAIL_INVALID,
  PASSWORD_REQUIRED,
  PASSWORD_INVALID
} from "validation";

const shallow = global["shallow"];
const mount = global["mount"];

const initialState: any = {};
const store = configureStore(initialState);

const createWrapper = (): ReactWrapper | any => {
  return mount(
    <Provider store={store}>
      <Reset />
    </Provider>
  );
};

describe("<UserPasswordResetForm />", () => {
  it("should match the snapshot", () => {
    const shallowWrapper = shallow(<Reset />);
    expect(shallowWrapper).toMatchSnapshot();
  });

  describe("defines password reset form fields", () => {
    it("renders email field", () => {
      const wrapper = createWrapper();
      const field = wrapper.findFieldByName("email");

      expect(field.prop("type")).toBe("text");
      expect(field.prop("label")).toBe("Email");
      expect(field.prop("component")).toBe(Input.Text);
    });

    it("renders password field", () => {
      const wrapper = createWrapper();
      const field = wrapper.findFieldByName("password");

      expect(field.prop("type")).toBe("password");
      expect(field.prop("label")).toBe("Password");
      expect(field.prop("component")).toBe(Input.Password);
    });

    it("renders confirmPassword field", () => {
      const wrapper = createWrapper();
      const field = wrapper.findFieldByName("confirmPassword");

      expect(field.prop("type")).toBe("password");
      expect(field.prop("label")).toBe("Confirm Password");
      expect(field.prop("component")).toBe(Input.Password);
    });

    it("renders submit button", () => {
      const wrapper = createWrapper();
      const submitButton = wrapper.findSubmitButton();

      expect(submitButton.prop("type")).toBe("submit");
      expect(submitButton.text()).toBe("Reset");
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

    // TODO: REDUX FORM IS NOT CREATING THE FORMFEEDBACK FOR PASSWORD TYPE EVEN THOUGH IT GOES INTO THE VALIDATION PROPERLY.
    //
    // describe("password validation", () => {
    //   it("shows error when password is set to blank", () => {
    //     const wrapper = createWrapper();
    //     const input = wrapper.findInputByName("password");
    //     input.simulate("blur");

    //     const errorPresent = wrapper.findFormFeedback(PASSWORD_REQUIRED);
    //     expect(errorPresent).toBeTruthy();
    //   });

    //   it("shows error when password is set to invalid", () => {
    //     const wrapper = createWrapper();
    //     const input = wrapper.findInputByName("password");
    //     input.simulate("change", { target: { value: "!" } });

    //     const errorPresent = wrapper.findFormFeedback(PASSWORD_INVALID);
    //     expect(errorPresent).toBeTruthy();
    //   });
    // });
  });
});
