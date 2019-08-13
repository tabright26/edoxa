import React, { Component } from "react";
import { TabPane } from "reactstrap";

import PersonalInfoTabPanel from "./PersonalInfo/TabPanel/TabPanel";
import EmailTabPanel from "./Email/TabPanel/TablPanel";
import PhoneNumberTabPanel from "./PhoneNumber/TabPanel/TablPanel";
import DoxaTagTabPanel from "./DoxaTag/TabPanel/TablPanel";
import AddressBookTabPanel from "./AddressBook/TabPanel/TablPanel";

class ProfileDetails extends Component {
  render() {
    const { tabId } = this.props;
    return (
      <>
        <TabPane tabId={tabId} className="mb-4 p-0">
          <h5>PROFILE DETAILS</h5>
        </TabPane>
        <PersonalInfoTabPanel tabId={tabId} />
        <EmailTabPanel tabId={tabId} />
        <PhoneNumberTabPanel tabId={tabId} />
        <DoxaTagTabPanel tabId={tabId} />
        <AddressBookTabPanel tabId={tabId} />
      </>
    );
  }
}

export default ProfileDetails;
