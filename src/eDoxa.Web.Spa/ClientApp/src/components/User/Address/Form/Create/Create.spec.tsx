import React from "react";
import { Provider } from "react-redux";
import { ReactWrapper } from "enzyme";
import Create from "./Create";
import { configureStore } from "store";
import { FormGroup } from "reactstrap";
import Input from "components/Shared/Input";
import {
  LINE1_REQUIRED,
  LINE1_INVALID,
  LINE2_INVALID,
  CITY_REQUIRED,
  CITY_INVALID,
  STATE_INVALID,
  POSTAL_INVALID
} from "validation";

const shallow = global["shallow"];
const mount = global["mount"];

const store = configureStore();

const createWrapper = (): ReactWrapper | any => {
  return mount(
    <Provider store={store}>
      <Create />
    </Provider>
  );
};

describe("<UserAddressCreateForm />", () => {
  it("should match the snapshot", () => {
    const shallowWrapper = shallow(<Create />);
    expect(shallowWrapper).toMatchSnapshot();
  });

  describe("defines address create form fields", () => {
    it("renders country field", () => {
      const wrapper = createWrapper();
      const field = wrapper.findFieldByName("country");

      expect(field.prop("type")).toBe("select");
      expect(field.prop("component")).toBe(Input.Select);
    });

    test.each([
      ["line1", "Address line 1"],
      ["line2", "Address line 2 (optional)"],
      ["city", "City"],
      ["state", "State"],
      ["postalCode", "Postal Code"]
    ])("renders name fields", (name: string, label: string) => {
      const wrapper = createWrapper();
      const field = wrapper.findFieldByName(name);

      expect(field.prop("type")).toBe("text");
      expect(field.prop("label")).toBe(label);
      expect(field.prop("formGroup")).toBe(FormGroup);
      expect(field.prop("component")).toBe(Input.Text);
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

  describe("form validation", () => {
    describe("text fields validation", () => {
      test.each([
        ["line1", LINE1_REQUIRED],
        ["city", CITY_REQUIRED]
      ])("name fields blank validation", (name: string, message: string) => {
        const wrapper = createWrapper();
        const input = wrapper.findInputByName(name);
        input.simulate("blur");

        const errorPresent = wrapper.findFormFeedback(message);
        expect(errorPresent).toBeTruthy();
      });

      test.each([
        ["line1", LINE1_INVALID],
        ["line2", LINE2_INVALID],
        ["city", CITY_INVALID],
        ["state", STATE_INVALID],
        ["postalCode", POSTAL_INVALID]
      ])("name fields blank validation", (name: string, message: string) => {
        const wrapper = createWrapper();
        const input = wrapper.findInputByName(name);
        input.simulate("change", { target: { value: "_!" } });

        const errorPresent = wrapper.findFormFeedback(message);
        expect(errorPresent).toBeTruthy();
      });
    });
  });
});
