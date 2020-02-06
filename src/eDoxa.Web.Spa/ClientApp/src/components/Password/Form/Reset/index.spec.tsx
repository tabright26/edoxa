import React from "react";
import { Provider } from "react-redux";
import { ReactWrapper } from "enzyme";
import Reset from ".";
import { configureStore } from "store";
import Input from "components/Shared/Input";
import { EMAIL_REQUIRED } from "utils/form/validators";
import { MemoryRouter } from "react-router-dom";
import {
  findFieldByName,
  findSubmitButton,
  findInputByName,
  findFormFeedback
} from "utils/test/helpers";

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
      const field = findFieldByName(wrapper, "email");

      expect(field.prop("type")).toBe("text");
      expect(field.prop("placeholder")).toBe("Email");
      expect(field.prop("component")).toBe(Input.Text);
    });

    it("renders password field", () => {
      const wrapper = createWrapper();
      const field = findFieldByName(wrapper, "password");

      expect(field.prop("type")).toBe("password");
      expect(field.prop("placeholder")).toBe("New password");
      expect(field.prop("component")).toBe(Input.Password);
    });

    it("renders confirmPassword field", () => {
      const wrapper = createWrapper();
      const field = findFieldByName(wrapper, "confirmPassword");

      expect(field.prop("type")).toBe("password");
      expect(field.prop("placeholder")).toBe("Confirm new password");
      expect(field.prop("component")).toBe(Input.Password);
    });

    it("renders submit button", () => {
      const wrapper = createWrapper();
      const submitButton = findSubmitButton(wrapper);

      expect(submitButton.prop("type")).toBe("submit");
      expect(submitButton.text()).toBe("Reset");
    });
  });

  describe("form validation", () => {
    describe("email validation", () => {
      it("shows error when email is set to blank", () => {
        const wrapper = createWrapper();
        const input = findInputByName(wrapper, "email");
        input.simulate("blur");

        const errorPresent = findFormFeedback(wrapper, EMAIL_REQUIRED);
        expect(errorPresent).toBeTruthy();
      });
    });
  });
});
