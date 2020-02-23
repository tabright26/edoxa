import React from "react";
import { Provider } from "react-redux";
import { ReactWrapper } from "enzyme";
import Update from "./index";
import store from "store";
import { FormGroup } from "reactstrap";
import Input from "components/Shared/Input";
import {
  DOXATAG_REQUIRED,
  DOXATAG_INVALID,
  DOXATAG_MIN_LENGTH_INVALID,
  DOXATAG_MAX_LENGTH_INVALID
} from "utils/form/validators";

import {
  findFieldByName,
  findSubmitButton,
  findInputByName,
  findFormFeedback
} from "test/helper";

const shallow = global["shallow"];
const mount = global["mount"];

const createWrapper = (): ReactWrapper | any => {
  return mount(
    <Provider store={store}>
      <Update handleCancel={() => {}} />
    </Provider>
  );
};

describe("<UserDoxatagUpdateForm />", () => {
  it("should match the snapshot", () => {
    const shallowWrapper = shallow(
      <Provider store={store}>
        <Update handleCancel={() => {}} />
      </Provider>
    );
    expect(shallowWrapper).toMatchSnapshot();
  });

  describe("defines doxatag update form fields", () => {
    it("renders name field", () => {
      const wrapper = createWrapper();
      const field = findFieldByName(wrapper, "name");

      expect(field.prop("type")).toBe("text");
      expect(field.prop("placeholder")).toBe("Name");
      expect(field.prop("formGroup")).toBe(FormGroup);
      expect(field.prop("component")).toBe(Input.Text);
    });

    it("renders submit button", () => {
      const wrapper = createWrapper();
      const button = findSubmitButton(wrapper);

      expect(button.prop("type")).toBe("submit");
      expect(button.text()).toBe("Save");
    });
  });

  describe("form validation", () => {
    describe("name validation", () => {
      it("shows error when name is set to blank", () => {
        const wrapper = createWrapper();
        const input = findInputByName(wrapper, "name");
        input.simulate("blur");

        const errorPresent = findFormFeedback(wrapper, DOXATAG_REQUIRED);
        expect(errorPresent).toBeTruthy();
      });

      it("shows error when name is not long enough", () => {
        const wrapper = createWrapper();
        const input = findInputByName(wrapper, "name");
        input.simulate("change", { target: { value: "_" } });

        const errorPresent = findFormFeedback(
          wrapper,
          DOXATAG_MIN_LENGTH_INVALID
        );
        expect(errorPresent).toBeTruthy();
      });

      it("shows error when name is too long", () => {
        const wrapper = createWrapper();
        const input = findInputByName(wrapper, "name");
        input.simulate("change", { target: { value: "_Doxatag_Test_1234" } });

        const errorPresent = findFormFeedback(
          wrapper,
          DOXATAG_MAX_LENGTH_INVALID
        );
        expect(errorPresent).toBeTruthy();
      });

      it("shows error when name is set to invalid", () => {
        const wrapper = createWrapper();
        const input = findInputByName(wrapper, "name");
        input.simulate("change", { target: { value: "_123" } });

        const errorPresent = findFormFeedback(wrapper, DOXATAG_INVALID);
        expect(errorPresent).toBeTruthy();
      });
    });
  });
});
