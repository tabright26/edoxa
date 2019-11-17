import { connect } from "react-redux";
import { loadUserAddressBook, createUserAddress } from "store/root/user/addressBook/actions";
import { UserAddressBookActions, CREATE_USER_ADDRESS_FAIL } from "store/root/user/addressBook/types";
import Create from "./Create";
import { throwSubmissionError } from "utils/form/types";

const mapDispatchToProps = (dispatch: any) => {
  return {
    createUserAddress: (data: any) =>
      dispatch(createUserAddress(data)).then((action: UserAddressBookActions) => {
        switch (action.type) {
          case CREATE_USER_ADDRESS_FAIL: {
            throwSubmissionError(action.error);
            break;
          }
          default: {
            dispatch(loadUserAddressBook());
            break;
          }
        }
      })
  };
};

export default connect(
  null,
  mapDispatchToProps
)(Create);
