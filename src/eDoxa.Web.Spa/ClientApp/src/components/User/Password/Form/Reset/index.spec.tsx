import React from "react";
import { Provider } from "react-redux";
import { ReactWrapper } from "enzyme";
import Reset from "./Reset";
import { configureStore } from "store";
import Input from "components/Shared/Input";
import { EMAIL_REQUIRED, EMAIL_INVALID } from "utils/form/validators";
import { MemoryRouter } from "react-router-dom";

const shallow = global["shallow"];
const mount = global["mount"];

const store = configureStore();

const createWrapper = (): ReactWrapper | any => {
  return mount(
    <Provider store={store}>
      <MemoryRouter>
        <Reset />
      </MemoryRouter>
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
      expect(field.prop("label")).toBe("New password");
      expect(field.prop("component")).toBe(Input.Password);
    });

    it("renders confirmPassword field", () => {
      const wrapper = createWrapper();
      const field = wrapper.findFieldByName("confirmPassword");

      expect(field.prop("type")).toBe("password");
      expect(field.prop("label")).toBe("Confirm new password");
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
  });
});
