import { connect } from "react-redux";
import { RootState } from "store/types";
import Update from "./Update";
import { loadUserPhone, updateUserPhone } from "store/root/user/phone/actions";
import { UPDATE_USER_PHONE_FAIL, UserPhoneActions } from "store/root/user/phone/types";
import { throwSubmissionError } from "utils/form/types";

const mapStateToProps = (state: RootState) => {
  const { data } = state.root.user.phone;
  return {
    initialValues: data
  };
};

const mapDispatchToProps = (dispatch: any) => {
  return {
    updateUserPhone: (data: any) =>
      dispatch(updateUserPhone(data)).then((action: UserPhoneActions) => {
        switch (action.type) {
          case UPDATE_USER_PHONE_FAIL: {
            throwSubmissionError(action.error);
            break;
          }
          default: {
            dispatch(loadUserPhone());
            break;
          }
        }
      })
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(Update);
