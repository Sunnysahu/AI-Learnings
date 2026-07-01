## 🔐 GitHub Personal Access Token Setup

To use this project, you need a GitHub Personal Access Token (PAT).

### 1. Generate a Token

Create a new token from your GitHub settings:

👉 https://github.com/settings/personal-access-tokens

Make sure to select only the permissions required for your use case.

---

### 2. Configure the Token in Your Project

⚠️ **Do NOT hardcode your token in the source code for production or public repositories.**

Instead, use one of the safer approaches below:

#### Option A: Environment Variable (Recommended)

Set your token as an environment variable:

**Windows (PowerShell):**
```powershell
setx GITHUB_TOKEN "your_token_here"