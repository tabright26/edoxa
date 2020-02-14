import React from "react";
import { Provider } from "react-redux";
import { ReactWrapper } from "enzyme";
import Update from ".";
import store from "store";
import { FormGroup } from "reactstrap";
import Input from "components/Shared/Input";
import {
  findFieldByName,
  findSubmitButton,
  findCancelButton
} from "utils/test/helpers";

const shallow = global["shallow"];
const mount = global["mount"];

const createWrapper = (): ReactWrapper | any => {
  return mount(
    <Provider store={store}>
      <Update handleCancel={() => {}} />
    </Provider>
  );
};

describe("<UserPhoneUpdateForm />", () => {
  it("should match the snapshot", () => {
    const shallowWrapper = shallow(
      <Provider store={store}>
        <Update handleCancel={() => {}} />
      </Provider>
    );
    expect(shallowWrapper).toMatchSnapshot();
  });

  describe("defines phone update form fields", () => {
    it("renders number field", () => {
      const wrapper = createWrapper();
      const field = findFieldByName(wrapper, "number");

      expect(field.prop("type")).toBe("text");
      expect(field.prop("placeholder")).toBe("Phone Number");
      expect(field.prop("formGroup")).toBe(FormGroup);
      expect(field.prop("component")).toBe(Input.Text);
    });

    it("renders submit button", () => {
      const wrapper = createWrapper();
      const submitButton = findSubmitButton(wrapper);

      expect(submitButton.prop("type")).toBe("submit");
      expect(submitButton.text()).toBe("Save");
    });

    it("renders cancel button", () => {
      const wrapper = createWrapper();
      const cancelButton = findCancelButton(wrapper);

      expect(cancelButton.prop("type")).toBe("button");
      expect(cancelButton.text()).toBe("Cancel");
    });
  });
});
