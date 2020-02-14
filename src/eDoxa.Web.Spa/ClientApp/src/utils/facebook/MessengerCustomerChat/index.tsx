import React from "react";
import { MessengerCustomerChat as FacebookMessengerCustomerChat } from "react-messenger-customer-chat";
import { REACT_APP_FACEBOOK_MCC_ENABLED } from "keys";

export const MessengerCustomerChat = () =>
  REACT_APP_FACEBOOK_MCC_ENABLED === "true" && (
    <FacebookMessengerCustomerChat
      pageId={process.env.REACT_APP_FACEBOOK_MCC_PAGE_ID}
      appId={process.env.REACT_APP_FACEBOOK_MCC_APP_ID}
      version="5.0"
      xfbml
      themeColor="#ff6f00"
    />
  );
