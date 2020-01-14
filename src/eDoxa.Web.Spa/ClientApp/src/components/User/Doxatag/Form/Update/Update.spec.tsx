import React from "react";
import { Provider } from "react-redux";
import { ReactWrapper } from "enzyme";
import Update from "./Update";
import { configureStore } from "store";
import { FormGroup } from "reactstrap";
import Input from "components/Shared/Input";
import {
  DOXATAG_REQUIRED,
  DOXATAG_INVALID,
  DOXATAG_MIN_LENGTH_INVALID,
  DOXATAG_MAX_LENGTH_INVALID
} from "validation";

const shallow = global["shallow"];
const mount = global["mount"];

const store = configureStore();

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
      const field = wrapper.findFieldByName("name");

      expect(field.prop("type")).toBe("text");
      expect(field.prop("label")).toBe("Name");
      expect(field.prop("formGroup")).toBe(FormGroup);
      expect(field.prop("component")).toBe(Input.Text);
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
    describe("name validation", () => {
      it("shows error when name is set to blank", () => {
        const wrapper = createWrapper();
        const input = wrapper.findInputByName("name");
        input.simulate("blur");

        const errorPresent = wrapper.findFormFeedback(DOXATAG_REQUIRED);
        expect(errorPresent).toBeTruthy();
      });

      it("shows error when name is not long enough", () => {
        const wrapper = createWrapper();
        const input = wrapper.findInputByName("name");
        input.simulate("change", { target: { value: "_" } });

        const errorPresent = wrapper.findFormFeedback(
          DOXATAG_MIN_LENGTH_INVALID
        );
        expect(errorPresent).toBeTruthy();
      });

      it("shows error when name is too long", () => {
        const wrapper = createWrapper();
        const input = wrapper.findInputByName("name");
        input.simulate("change", { target: { value: "_Doxatag_Test_1234" } });

        const errorPresent = wrapper.findFormFeedback(
          DOXATAG_MAX_LENGTH_INVALID
        );
        expect(errorPresent).toBeTruthy();
      });

      it("shows error when name is set to invalid", () => {
        const wrapper = createWrapper();
        const input = wrapper.findInputByName("name");
        input.simulate("change", { target: { value: "_123" } });

        const errorPresent = wrapper.findFormFeedback(DOXATAG_INVALID);
        expect(errorPresent).toBeTruthy();
      });
    });
  });
});
